### Examples

#### Getting Started
To start using the library first instantiate the controller.
```c#
// Option 1
Controller controller = new Controller("Example Matter Fabric");

// Option 2
Controller controller = Controller.Load("example.fabric", "example.key");
```
_Option 1: Creates a new controller then generates the fabric named "Example Matter Fabric" and the certificates._\
_Option 2: Load an existing fabric into a controller._

#### Example 1: Interviewing the Fabric
On first startup of a fabric or after a long disconnected period it is a good idea to re-enumerate the fabric.  This will interview each node on the fabric for some basic information.
```c#
await controller.EnumerateFabric();
```
  
#### Example 2: Adding Nodes (Commissioning an already commissioned device):
This example adds a node to our fabric that has already been setup by another device. To commission a node, we need a commissioning payload. This is generated with either an 11/21 digit PIN Code, a QR code or an NFC tag.
```c#
// Option 1
CommissioningPayload payload = CommissioningPayload.FromPIN("00362159269");

// Option 2
CommissioningPayload payload = CommissioningPayload.FromQR("MT:Y.K9042C00KA0648G00");

// Option 3
CommissioningPayload payload = CommissioningPayload.FromNFC(nfcBytes);
```
_Option 1: Creates a commissioning payload from a PIN Number._\
_Option 2: Creates a commissioning payload from a QR Code._\
_Option 3: Creates a commissioning payload from an NFC tag._

Once we have the payload, we can activate commissioning which will automatically search bluetooth and/or the network and setup the device.

```c#
    CommissioningState state = await controller.StartCommissioning(payload);
    await controller.CompleteCommissioning(state);
    controller.Save("example.fabric", "example.key");
```
_Line 1 Find the device and begin commissioning._\
_Line 2 Complete commissioning._\
_Line 3 Save the fabric with the certificates for the newly added node._

#### Example 3: Adding Nodes (Commissioning a New/Reset Device):
To commission a node, we need a commissioning payload. This is generated with either an 11/21 digit PIN Code, a QR code or an NFC tag.
```c#
// Option 1
CommissioningPayload payload = CommissioningPayload.FromPIN("00362159269");

// Option 2
CommissioningPayload payload = CommissioningPayload.FromQR("MT:Y.K9042C00KA0648G00");

// Option 3
CommissioningPayload payload = CommissioningPayload.FromNFC(nfcBytes);
```
_Option 1: Creates a commissioning payload from a PIN Number._\
_Option 2: Creates a commissioning payload from a QR Code._\
_Option 3: Creates a commissioning payload from an NFC tag._

Once we have the payload, we can activate commissioning which will automatically search bluetooth and/or the network and setup the device.

```c#
    CommissioningState state = await controller.StartCommissioning(payload);
    var network = state.FindWiFi("Linksys-24G")!;
    await controller.CompleteCommissioning(state, network, "password123");
    controller.Save("example.fabric", "example.key");
```
_Line 1 Find the device and begin commissioning._\
_Line 2 Select a WiFi network out of the networks the device can see. This step can be skipped if the device is already on the network._\
_Line 3 Complete commissioning and connect to the provided WiFi network._\
_Line 4 Save the fabric with the certificates for the newly added node._

Alternatively, line 2 can be replaced by selecting a thread network or an ethernet connection.