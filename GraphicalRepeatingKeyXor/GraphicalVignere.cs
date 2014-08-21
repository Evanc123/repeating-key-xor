using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalRepeatingKeyXor
{
    public partial class GraphicalVignere : Form
    {
        public GraphicalVignere()
        {
            InitializeComponent();
        }

        private void Encrypt(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(inputTextBox.Text) || string.IsNullOrEmpty(keyTextBox.Text)) return;
            var key = StringToByteArray(keyTextBox.Text);
            var input = StringToByteArray(inputTextBox.Text);
            var encryptedBytes = BitConverter.ToString(RepeatingKeyXorFunc(input, key));
            outputTextBox.Text = encryptedBytes;
            outputTextBox.Visible = true;
            outputLabel.Visible = true;
        }
        private static byte[] RepeatingKeyXorFunc(IList<byte> bytesToEncrypt, IList<byte> key)
        {
            var encryptedByteArray = new byte[bytesToEncrypt.Count];
            var keyCount = 0;
            for (var i = 0; i < bytesToEncrypt.Count; i++)
            {
                encryptedByteArray[i] = (keyCount == key.Count)
                    ? Xor(bytesToEncrypt[i], key[keyCount = 0])
                    : Xor(bytesToEncrypt[i], key[keyCount]);

                keyCount++;
            }
            return encryptedByteArray;
        }

        private static byte[] StringToByteArray(string inString)
        {
            char[] characters = inString.ToCharArray();
            var byteArray = new byte[characters.Length];
            var index = 0;
            foreach (var character in characters)
            {
                byteArray[index] = (byte)character;
                index++;
            }
            return byteArray;
        }
        static byte[] ConvertFromHexToByteArray(string hex)
        {
            var inputArray = new byte[hex.Length / 2];
            for (var i = 0; i < inputArray.Length; i++)
            {
                inputArray[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return inputArray;
        }
        private static byte Xor(byte b, int c)
        {
            return (byte)(b ^ c);
        }

        private void Decrypt(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(inputTextBox.Text) || string.IsNullOrEmpty(keyTextBox.Text)) return;
            var key = StringToByteArray(keyTextBox.Text);
            var input = ConvertFromHexToByteArray(inputTextBox.Text.Replace("-", string.Empty));
            outputTextBox.Text = String.Empty;
            var outputText = System.Text.Encoding.ASCII.GetString(RepeatingKeyXorFunc(input, key));
            outputTextBox.Text = outputText;
            outputTextBox.Visible = true;
            outputLabel.Visible = true;
        }
    }
}


