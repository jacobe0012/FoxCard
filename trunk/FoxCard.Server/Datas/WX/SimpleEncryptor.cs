using System;

namespace FoxCard.Server.Datas;

public class MyEncryptor
{
    private const int Shift = 5; // 可以根据需要调整偏移量

    public static string Encrypt(string input)
    {
        return ShiftCharacters(input, Shift);
    }

    public static string Decrypt(string input)
    {
        return ShiftCharacters(input, -Shift);
    }

    private static string ShiftCharacters(string input, int shift)
    {
        char[] buffer = input.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char ch = buffer[i];
            if (ch >= 'a' && ch <= 'z')
            {
                buffer[i] = (char)((ch - 'a' + shift + 26) % 26 + 'a');
            }
            else if (ch >= 'A' && ch <= 'Z')
            {
                buffer[i] = (char)((ch - 'A' + shift + 26) % 26 + 'A');
            }
            else if (ch >= '0' && ch <= '9')
            {
                buffer[i] = (char)((ch - '0' + shift + 10) % 10 + '0');
            }
            // 处理其他字符（如 '-'）保持不变
        }

        return new string(buffer);
    }
}