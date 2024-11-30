
namespace MatterDotNet.Protocol.Payloads
{
    public enum GeneralCode
    {
        /// <summary>
        /// Operation completed successfully.
        /// </summary>
        SUCCESS = 0, 
        /// <summary>
        /// Generic failure, additional details may be included in the protocol specific status.
        /// </summary>
        FAILURE = 1, 
        /// <summary>
        /// Operation was rejected by the system because the system is in an invalid state.
        /// </summary>
        BAD_PRECONDITION = 2, 
        /// <summary>
        /// A value was out of a required range
        /// </summary>
        OUT_OF_RANGE = 3, 
        /// <summary>
        /// A request was unrecognized or malformed
        /// </summary>
        BAD_REQUEST = 4, 
        /// <summary>
        /// An unrecognized or unsupported request was received
        /// </summary>
        UNSUPPORTED = 5, 
        /// <summary>
        /// A request was not expected at this time
        /// </summary>
        UNEXPECTED = 6, 
        /// <summary>
        /// Insufficient resources to process the given request
        /// </summary>
        RESOURCE_EXHAUSTED = 7, 
        /// <summary>
        /// Device is busy and cannot handle this request at this time
        /// </summary>
        BUSY = 8, 
        /// <summary>
        /// A timeout occurred
        /// </summary>
        TIMEOUT = 9, 
        /// <summary>
        /// Context-specific signal to proceed
        /// </summary>
        CONTINUE = 10, 
        /// <summary>
        /// Failure, may be due to a concurrency error.
        /// </summary>
        ABORTED = 11, 
        /// <summary>
        /// An invalid/unsupported argument was provided
        /// </summary>
        INVALID_ARGUMENT = 12, 
        /// <summary>
        /// Some requested entity was not found
        /// </summary>
        NOT_FOUND = 13, 
        /// <summary>
        /// The sender attempted to create something that already exists
        /// </summary>
        ALREADY_EXISTS = 14, 
        /// <summary>
        /// The sender does not have sufficient permissions to execute the requested operations.
        /// </summary>
        PERMISSION_DENIED = 15, 
        /// <summary>
        /// Unrecoverable data loss or corruption has occurred.
        /// </summary>
        DATA_LOSS = 16, 
        /// <summary>
        /// Message size is larger than the recipient can handle.
        /// </summary>
        MESSAGE_TOO_LARGE = 17, 
    }
}
