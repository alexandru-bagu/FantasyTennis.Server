public static class ByteArrayExtensions
{
    public static bool IsZero(this byte[] buffer)
    {
        for (int i = 0; i < buffer.Length; i++)
            if (buffer[i] != 0)
                return false;
        return true;
    }
}
