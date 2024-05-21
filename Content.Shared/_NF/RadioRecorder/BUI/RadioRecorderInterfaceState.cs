using Robust.Shared.Serialization;

namespace Content.Shared._NF.RadioRecorder.BUI;

[NetSerializable, Serializable]
public sealed class RadioRecorderInterfaceState : BoundUserInterfaceState
{
    /// <summary>
    /// The name of the speaker of a radio message that is being searched for
    /// </summary>
    public string SpeakerName;
    /// <summary>
    /// A portion of a radio message that is being searched for
    /// </summary>
    public string MessageSubstring;
    /// <summary>
    /// The earliest radio message timestamp, in minutes, that is being searched for
    /// </summary>
    public int TimeStart;
    /// <summary>
    /// The latest radio message timestamp, in minutes, that is being searched for
    /// </summary>
    public int TimeEnd;


    public RadioRecorderInterfaceState(string speakerName, string messageSubstring, int timeStart, int timeEnd)
    {
        SpeakerName = speakerName;
        MessageSubstring = messageSubstring;
        TimeStart = timeStart;
        TimeEnd = timeEnd;
    }
}