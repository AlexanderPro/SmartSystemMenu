namespace SmartSystemMenu.HotKeys
{
    public enum WindowCloserType : int
    {
        CloseForegroundWindow = 0x00,
        CloseWindowUnderCursor = 0x01,
        KillProcessWithForegroundWindow = 0x02,
        KillProcessWithWindowUnderCursor = 0x03
    }
}
