// MatterDotNet Copyright (C) 2024 
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using MatterDotNet.Security;
using System.Numerics;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Cryptography
{
    public struct BigIntegerPoint : IEquatable<BigIntegerPoint>
    {
        private static readonly BigInteger SIGN_BIT = 1 << 256;
        public BigIntegerPoint() { }
        public BigIntegerPoint(BigInteger x, BigInteger y)
        {
            X = x;
            Y = y;
        }
        public BigIntegerPoint(byte[] x, byte[] y)
        {
            X = new BigInteger(x, true, true);
            Y = new BigInteger(y, true, true);
        }

        public BigIntegerPoint(byte[] point)
        {
            switch (point[0])
            {
                case 2:
                case 3:
                    X = new BigInteger(point.AsSpan(1), true, true);
                    Y = BigIntUtil.ModSqrt((BigInteger.ModPow(X, 3, SecP256.p) + (SecP256.a * X) % SecP256.p + SecP256.b) % SecP256.p, SecP256.p);
                    if (point[0] == 0x3)
                        Y |= SIGN_BIT;
                    break;
                case 4:
                    int len = (point.Length - 1) / 2;
                    X = new BigInteger(point.AsSpan(1, len), true, true);
                    Y = new BigInteger(point.AsSpan(len + 1, len), true, true);
                    break;
                default:
                    throw new ArgumentException("Invalid Point Type: " + point[0], nameof(point));
            }
        }

        public BigInteger X { get; set; }
        public BigInteger Y { get; set; }

        public ReadOnlySpan<byte> ToBytes(bool compressed)
        {
            if (compressed)
            {
                Span<byte> ret = new byte[33];
                ret[0] = ((Y & SIGN_BIT) == SIGN_BIT) ? (byte)0x3 : (byte)0x2;
                X.TryWriteBytes(ret.Slice(1), out _, true, true);
                return ret;
            }
            else
            {
                Span<byte> ret = new byte[65];
                ret[0] = 0x4;
                X.TryWriteBytes(ret.Slice(1), out _, true, true);
                Y.TryWriteBytes(ret.Slice(33), out _, true, true);
                return ret;
            }
        }

        public ECPoint ToECPoint()
        {
            ECPoint p = new ECPoint();
            byte[] x = new byte[32];
            X.TryWriteBytes(x, out _, true, true);
            p.X = x;
            byte[] y = new byte[32];
            Y.TryWriteBytes(y, out _, true, true);
            p.Y = y;
            return p;
        }

        public void Negate()
        {
            Y ^= SIGN_BIT;
        }

        public bool Equals(BigIntegerPoint other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }
    }
}
