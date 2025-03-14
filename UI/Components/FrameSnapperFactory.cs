using System;
using LiveSplit.Model;
using LiveSplit.UI.Components;
using UpdateManager;

[assembly: ComponentFactory(typeof(FrameSnapperFactory))]

namespace LiveSplit.UI.Components;
public class FrameSnapperFactory : IComponentFactory, IUpdateable
{
    public string ComponentName => "60 FPS Frame Snapper";

    public string Description => "Snaps split times to nearest multiple of 1/60th of a second.";

    public ComponentCategory Category => ComponentCategory.Control;

    public string UpdateName => ComponentName;

    public string XMLURL => "http://livesplit.org/update/Components/noupdates.xml";

    public string UpdateURL => "http://livesplit.org/update/";

    public Version Version => Version.Parse("1.0");

    public IComponent Create(LiveSplitState state)
    {
        return new FrameSnapper(state);
    }
}
