public static class CmdHelper
{
    //获取cmd
    public static int GetCmd(int merge)
    {
        return merge >> 16;
    }

    //获取subCmd
    public static int GetSubCmd(int merge)
    {
        return merge & 0xFFFF;
    }

    //获取mergeCmd
    public static int GetMergeCmd(int cmd, int subCmd)
    {
        return (cmd << 16) + subCmd;
    }
}