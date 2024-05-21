using Content.Shared._NF.RadioRecorder;
using Content.Shared._NF.RadioRecorder.Events;
using JetBrains.Annotations;
using Robust.Client.GameObjects;

namespace Content.Client._NF.RadioRecorder.UI;
[UsedImplicitly]
public sealed class RadioRecorderBoundUserInterface : BoundUserInterface
{
    [ViewVariables]
    private RadioRecorderWindow? _window;

    public RadioRecorderBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();

        _window = new RadioRecorderWindow(this);

        /*
        _window.Switch.OnPressed += _ =>
        {
            SendMessage(new SharedGravityGeneratorComponent.SwitchGeneratorMessage(!IsOn));
        };
        */

        _window.OpenCentered();
        _window.OnClose += Close;
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        var castState = (SharedGravityGeneratorComponent.GeneratorState) state;
        _window?.UpdateState(castState);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (!disposing) return;

        _window?.Dispose();
    }

    public void SetRadioRecorderFilterParameters(bool on)
    {
        SendMessage(new RadioRecorderFilterMessage());
    }
}

