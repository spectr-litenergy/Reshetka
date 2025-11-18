using System;
using System.Text;
using System.Windows.Forms;

namespace Reshetka
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            numericSize.Value = 4;
            textMask.Text = "XXX.\n.X..\n....\n....";
        }

        private void numericSize_ValueChanged(object? sender, EventArgs e)
        {
            int size = (int)numericSize.Value;
            if ((size % 2) == 1) return;
            textMask.Text = GenerateDefaultMask(size);
        }

        private void numericSize_Validated(object? sender, EventArgs e)
        {
            int size = (int)numericSize.Value;
            if ((size % 2) == 1)
            {
                numericSize.Value = Math.Max(4, size - 1);
            }
            int even = (int)numericSize.Value;
            textMask.Text = GenerateDefaultMask(even);
        }

        private void buttonEncrypt_Click(object? sender, EventArgs e)
        {
            try
            {
                int size = (int)numericSize.Value;
                bool[,] mask = ParseMask(textMask.Text, size);
                string plain = textPlain.Text;
                string cipher = EncryptWithGrille(plain, mask);
                textCipher.Text = cipher;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDecrypt_Click(object? sender, EventArgs e)
        {
            try
            {
                int size = (int)numericSize.Value;
                bool[,] mask = ParseMask(textMask.Text, size);
                string cipher = textCipher.Text;
                string plain = DecryptWithGrille(cipher, mask).TrimEnd('X');
                textPlain.Text = plain;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSwap_Click(object? sender, EventArgs e)
        {
            string a = textPlain.Text;
            textPlain.Text = textCipher.Text;
            textCipher.Text = a;
        }

        private void buttonClear_Click(object? sender, EventArgs e)
        {
            textPlain.Clear();
            textCipher.Clear();
        }

        internal static string GenerateDefaultMask(int size)
        {
            if ((size % 2) == 1)
                throw new ArgumentException("Размер N должен быть чётным.");
            bool[,] holes = new bool[size, size];
            var sb = new StringBuilder(size * (size + 1));

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    
                    var p0 = (R: r, C: c);
                    var p1 = (R: c, C: size - 1 - r);
                    var p2 = (R: size - 1 - r, C: size - 1 - c);
                    var p3 = (R: size - 1 - c, C: r);

              
                    var min = p0;
                    if (Compare(p1, min) < 0) min = p1;
                    if (Compare(p2, min) < 0) min = p2;
                    if (Compare(p3, min) < 0) min = p3;
                    holes[r, c] = (r == min.R && c == min.C);
                }
            }

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                    sb.Append(holes[r, c] ? 'X' : '.');
                if (r < size - 1) sb.Append('\n');
            }
            return sb.ToString();

            static int Compare((int R, int C) a, (int R, int C) b)
            {
                if (a.R != b.R) return a.R.CompareTo(b.R);
                return a.C.CompareTo(b.C);
            }
        }

        internal static bool[,] ParseMask(string maskText, int size)
        {
            string[] lines = maskText.Replace("\r", string.Empty).Split('\n');
            if (lines.Length != size)
                throw new ArgumentException($"Ожидалось {size} строк в маске.");

            bool[,] mask = new bool[size, size];
            for (int r = 0; r < size; r++)
            {
                if (lines[r].Length != size)
                    throw new ArgumentException($"Строка {r + 1} должна иметь длину {size}.");
                for (int c = 0; c < size; c++)
                {
                    char ch = lines[r][c];
                    mask[r, c] = ch == 'X' || ch == 'x' || ch == '1' || ch == '*';
                }
            }
            ValidateMaskUniqueOnRotations(mask);
            return mask;
        }

        internal static void ValidateMaskUniqueOnRotations(bool[,] mask)
        {
            int n = mask.GetLength(0);
            bool[,] rotated = (bool[,])mask.Clone();
            var used = new bool[n, n];
            for (int turn = 0; turn < 4; turn++)
            {
                for (int r = 0; r < n; r++)
                {
                    for (int c = 0; c < n; c++)
                    {
                        if (!rotated[r, c]) continue;
                        if (used[r, c])
                            throw new ArgumentException("Маска некорректна: отверстия совпадают при поворотах.");
                        used[r, c] = true;
                    }
                }
                rotated = Rotate90(rotated);
            }
        }

        internal static bool[,] Rotate90(bool[,] src)
        {
            int n = src.GetLength(0);
            bool[,] dst = new bool[n, n];
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    dst[c, n - 1 - r] = src[r, c];
            return dst;
        }

        internal static string EncryptWithGrille(string plain, bool[,] mask)
        {
            int n = mask.GetLength(0);
            int totalCells = n * n;
            string normalized = NormalizeText(plain);
            if (normalized.Length % totalCells != 0)
            {
                int pad = totalCells - (normalized.Length % totalCells);
                normalized = normalized + new string('X', pad);
            }

            var output = new StringBuilder(normalized.Length);
            for (int blockStart = 0; blockStart < normalized.Length; blockStart += totalCells)
            {
                char[,] grid = new char[n, n];
                int idx = blockStart;
                bool[,] current = (bool[,])mask.Clone();
                for (int turn = 0; turn < 4; turn++)
                {
                    for (int r = 0; r < n; r++)
                    {
                        for (int c = 0; c < n; c++)
                        {
                            if (current[r, c])
                            {
                                grid[r, c] = idx < blockStart + totalCells ? normalized[idx++] : 'X';
                            }
                        }
                    }
                    current = Rotate90(current);
                }

                for (int r = 0; r < n; r++)
                    for (int c = 0; c < n; c++)
                        output.Append(grid[r, c] == '\0' ? 'X' : grid[r, c]);
            }
            return output.ToString();
        }

        internal static string DecryptWithGrille(string cipher, bool[,] mask)
        {
            int n = mask.GetLength(0);
            int totalCells = n * n;
            if (cipher.Length % totalCells != 0)
                throw new ArgumentException($"Длина шифртекста должна быть кратна {totalCells}.");

            var output = new StringBuilder(cipher.Length);
            for (int blockStart = 0; blockStart < cipher.Length; blockStart += totalCells)
            {
                char[,] grid = new char[n, n];
                int idx = blockStart;
                for (int r = 0; r < n; r++)
                    for (int c = 0; c < n; c++)
                        grid[r, c] = cipher[idx++];

                bool[,] current = (bool[,])mask.Clone();
                var blockPlain = new StringBuilder(totalCells);
                for (int turn = 0; turn < 4; turn++)
                {
                    for (int r = 0; r < n; r++)
                    {
                        for (int c = 0; c < n; c++)
                        {
                            if (current[r, c])
                            {
                                blockPlain.Append(grid[r, c]);
                            }
                        }
                    }
                    current = Rotate90(current);
                }
                output.Append(blockPlain.ToString());
            }
            return output.ToString();
        }

        internal static string NormalizeText(string text)
        {
            var sb = new StringBuilder(text.Length);
            foreach (char ch in text)
            {
                if (char.IsLetterOrDigit(ch)) sb.Append(char.ToUpperInvariant(ch));
                else if (char.IsWhiteSpace(ch)) continue;
                else sb.Append(ch);
            }
            return sb.ToString();
        }

        private void buttonVisualize_Click(object? sender, EventArgs e)
        {
            try
            {
                int n = (int)numericSize.Value;
                bool[,] mask = ParseMask(textMask.Text, n);
                using (var dlg = new GrillePreviewForm(mask))
                {
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    dlg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class GrillePreviewForm : Form
        {
            private readonly bool[,] _mask;
            private int _rotation;
            private readonly Button _rotateButton;

            public GrillePreviewForm(bool[,] mask)
            {
                _mask = (bool[,])mask.Clone();
                Text = "Визуализация решётки";
                ClientSize = new System.Drawing.Size(420, 460);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;

                _rotateButton = new Button { Text = "Повернуть решетку", Dock = DockStyle.Bottom, Height = 36 };
                _rotateButton.Click += (s, e) => { _rotation = (_rotation + 1) % 4; Invalidate(); };
                Controls.Add(_rotateButton);

                DoubleBuffered = true;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                var g = e.Graphics;
                g.Clear(System.Drawing.Color.White);

                int n = _mask.GetLength(0);
                int margin = 20;
                int size = Math.Min(ClientSize.Width - margin * 2, ClientSize.Height - margin * 3 - _rotateButton.Height);
                int cell = Math.Max(8, size / n);
                int gridSize = cell * n;
                int left = (ClientSize.Width - gridSize) / 2;
                int top = margin;

                using (var pen = new System.Drawing.Pen(System.Drawing.Color.Gray, 1))
                using (var brushHole = new System.Drawing.SolidBrush(System.Drawing.Color.SteelBlue))
                using (var brushText = new System.Drawing.SolidBrush(System.Drawing.Color.Black))
                using (var font = new System.Drawing.Font("Segoe UI", 9f))
                {
                    bool[,] current = (bool[,])_mask.Clone();
                    for (int i = 0; i < _rotation; i++) current = Rotate90(current);

                    for (int r = 0; r < n; r++)
                    {
                        for (int c = 0; c < n; c++)
                        {
                            int x = left + c * cell;
                            int y = top + r * cell;
                            g.DrawRectangle(pen, x, y, cell, cell);
                            if (current[r, c])
                            {
                                g.FillRectangle(brushHole, x + 1, y + 1, cell - 1, cell - 1);
                            }
                        }
                    }

                    string caption = $"N={n}, поворот: {_rotation * 90}°";
                    g.DrawString(caption, font, brushText, left, top + gridSize + 8);
                }
            }
        }
    }
}
