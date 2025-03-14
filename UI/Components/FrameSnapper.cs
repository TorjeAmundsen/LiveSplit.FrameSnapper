using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

using LiveSplit.Model;

namespace LiveSplit.UI.Components;

public class FrameSnapper : IComponent, IDisposable
{
    private TimeStamp PauseTime;

    public float PaddingTop => 0f;

    public float PaddingLeft => 0f;

    public float PaddingBottom => 0f;

    public float PaddingRight => 0f;

    protected ITimerModel Model { get; set; }

    protected LiveSplitState CurrentState { get; set; }

    public float VerticalHeight => 0f;

    public float MinimumWidth => 0f;

    public float HorizontalWidth => 0f;

    public float MinimumHeight => 0f;

    public string ComponentName => "60 FPS Frame Snapper";

    public IDictionary<string, Action> ContextMenuControls => null;

    public FrameSnapper(LiveSplitState state)
    {
        PauseTime = TimeStamp.Now;
        var timerModel = new TimerModel();
        timerModel.CurrentState = state;
        state.OnSplit += State_OnSplit;
        Model = timerModel;
        CurrentState = state;
    }

    private void State_OnSplit(object sender, EventArgs e)
    {
        var lastSplit = CurrentState.Run[CurrentState.CurrentSplitIndex - 1].SplitTime;
        int totalSeconds = (int)Math.Floor(lastSplit[TimingMethod.RealTime].Value.TotalSeconds);
        double adjustedDecimal = Math.Round((lastSplit[TimingMethod.RealTime].Value.TotalSeconds - totalSeconds) * 60) / 60.0;
        double adjustedTime = totalSeconds + adjustedDecimal;
        lastSplit[TimingMethod.RealTime] = TimeSpan.FromSeconds(adjustedTime);
        CurrentState.Run[CurrentState.CurrentSplitIndex - 1].SplitTime = lastSplit;
    }
    public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
    {
    }

    public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
    {
    }

    public Control GetSettingsControl(LayoutMode mode)
    {
        return null;
    }

    public void SetSettings(XmlNode settings)
    {
    }

    public XmlNode GetSettings(XmlDocument document)
    {
        return document.CreateElement("FrameSnapperSettings");
    }

    public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
    {
    }

    public void Dispose()
    {
        CurrentState.OnSplit -= State_OnSplit;
    }
}
