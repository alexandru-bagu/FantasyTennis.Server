using System.Security.Cryptography;

namespace Org.BouncyCastle.Crypto.Engines
{
    public class FantasyTennisRijndaelEngine : RijndaelEngine, ICryptoTransform
    {
        public bool CanReuseTransform => false;

        public bool CanTransformMultipleBlocks => false;

        public int InputBlockSize => 16;

        public int OutputBlockSize => 16;

        public void Dispose() { }

        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            return ProcessBlock(inputBuffer, inputOffset, outputBuffer, outputOffset);
        }

        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            byte[] output = new byte[OutputBlockSize];
            ProcessBlock(inputBuffer, inputOffset, output, 0);
            return output;
        }
    }
}
