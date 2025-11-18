using Reshetka;

namespace Reshetka.Tests;

public class GrilleTests
{
    [Fact]
    public void NormalizeText_RemovesWhitespace_UppercasesLetters()
    {
        var input = "Привет, мир! abc xyz 123\n\t";
        var result = MainForm.NormalizeText(input);
        Assert.Equal("ПРИВЕТ,МИР!ABCXYZ123", result);
    }

    [Fact]
    public void EncryptDecrypt_RoundTrip_ReturnsOriginal()
    {
        int n = 4;
        string maskText = MainForm.GenerateDefaultMask(n);
        bool[,] mask = MainForm.ParseMask(maskText, n);

        var plain = "HELLO WORLD";
        var cipher = MainForm.EncryptWithGrille(plain, mask);
        var decrypted = MainForm.DecryptWithGrille(cipher, mask).TrimEnd('X');

        Assert.Equal(MainForm.NormalizeText(plain).TrimEnd('X'), decrypted);
    }
}