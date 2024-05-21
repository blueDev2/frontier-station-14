using Robust.Shared.Serialization;

namespace Content.Shared._NF.RadioRecorder.Events;

/// <summary>
/// Raised when client changes the filter parameters for the Radio Recorder
/// </summary>
[Serializable, NetSerializable]

public sealed class RadioRecorderFilterMessage : BoundUserInterfaceMessage
{
    public RadioRecorderFilterMessage()
    {

    }
}
