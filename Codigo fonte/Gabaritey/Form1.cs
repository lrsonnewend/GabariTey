using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gabaritey
{
    public partial class Form1 : Form
    {
        Otsu ot = new Otsu();
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private Bitmap org;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Abre a caixa de dialogo do windows
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Buscar imagem da pasta, pelo nome do arquivo
                pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
                //Encapsula um bitmap, que consiste nos dados de pixel de uma imagem de elementos gráficos e seus atributos
                org = (Bitmap)pictureBox1.Image.Clone();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Encapsula um bitmap, onde estao os dados do pixel e seus atributos e faz uma copia
            Bitmap temp = (Bitmap)org.Clone();
            //Chama o metodo que converte em escala de cinza
            ot.Convert2GrayScaleFast(temp);
            //Chama o metodo que pega a limiarização
            int otsuThreshold = ot.getOtsuThreshold((Bitmap)temp);
            //Realiza o limiarização
            ot.threshold(temp, otsuThreshold);
            //Adiciona a imagem a caixa de imagem
            pictureBox1.Image = temp;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Abre a caixa de dialogo do windows para salvar a imagem.
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Salva a imagem da caixa de imagem
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Ajusta a imagem ao tamanho da caixa de imagem
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //Encapsula um bitmap, que consiste nos dados de pixel de uma imagem de elementos gráficos e seus atributos
            org = (Bitmap)pictureBox1.Image.Clone();
            //Metodo para carregar os itens da tabela
            carregaTabela();
                
        }


        private void button5_Click(object sender, EventArgs e)
        {
            try {
                Bitmap image1;
                // associa variável a imagem do picturebox1
                image1 = (Bitmap)pictureBox1.Image;
                int x;
                int y;

                int erros = 0;
                int acertos = 0;

                int valorReconhece = 68500;
                //Instancia uma matriz
                int[,] gabarito = new int[5, 5];
                int[,] resposta = new int[5, 5];

                //Pega o valor da celula indicada e coloca a matriz indicada
                gabarito[0, 0] = Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value.ToString());
                gabarito[0, 1] = Convert.ToInt32(dataGridView1.Rows[0].Cells[2].Value.ToString());
                gabarito[0, 2] = Convert.ToInt32(dataGridView1.Rows[0].Cells[3].Value.ToString());

                //2
                gabarito[1, 0] = Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value.ToString());
                gabarito[1, 1] = Convert.ToInt32(dataGridView1.Rows[1].Cells[2].Value.ToString());
                gabarito[1, 2] = Convert.ToInt32(dataGridView1.Rows[1].Cells[3].Value.ToString());

                //3
                gabarito[2, 0] = Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value.ToString());
                gabarito[2, 1] = Convert.ToInt32(dataGridView1.Rows[2].Cells[2].Value.ToString());
                gabarito[2, 2] = Convert.ToInt32(dataGridView1.Rows[2].Cells[3].Value.ToString());

                //4
                gabarito[3, 0] = Convert.ToInt32(dataGridView1.Rows[3].Cells[1].Value.ToString());
                gabarito[3, 1] = Convert.ToInt32(dataGridView1.Rows[3].Cells[2].Value.ToString());
                gabarito[3, 2] = Convert.ToInt32(dataGridView1.Rows[3].Cells[3].Value.ToString());

                //5
                gabarito[4, 0] = Convert.ToInt32(dataGridView1.Rows[4].Cells[1].Value.ToString());
                gabarito[4, 1] = Convert.ToInt32(dataGridView1.Rows[4].Cells[2].Value.ToString());
                gabarito[4, 2] = Convert.ToInt32(dataGridView1.Rows[4].Cells[3].Value.ToString());

               
                int somatoria = 0;
                //Percorre os pixels da linha
                for (y = 230; y <= 577; y++){
                    //Percorre os pixels da coluna
                    for (x = 99; x < 493; x++){
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0)){
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            //Soma 1 a cada vez passa por um pixels
                            somatoria++;
                        }
                        //Define a cor na imagem
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //Pega a somatoria de pixels e faz a media de pixels pretos
                if (somatoria > valorReconhece)
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value.ToString()) == 1) acertos++;
                }


                //quadro 2
                somatoria = 0;
                for (y = 230; y <= 577; y++)
                {
                    for (x = 753; x < 1082; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[0].Cells[2].Value.ToString());
                // linha 0 coluna 2
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[0].Cells[2].Value.ToString()) == 1) acertos++;
                }


                //quadro 3
                somatoria = 0;
                for (y = 230; y <= 577; y++)
                {
                    for (x = 1362; x < 1714; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[0].Cells[3].Value.ToString());
                // linha 0 coluna 3
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[0].Cells[3].Value.ToString()) == 1) acertos++;

                }

                //quadro 4
                somatoria = 0;
                for (y = 876; y <= 1194; y++)
                {
                    for (x = 99; x < 493; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[1].Cells[1].Value.ToString());
                // linha 1 coluna 1
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value.ToString()) == 1) acertos++;
                }
               

                //quadro 5
                somatoria = 0;
                for (y = 876; y <= 1194; y++)
                {
                    for (x = 753; x < 1082; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }
                //MessageBox.Show(dataGridView1.Rows[1].Cells[2].Value.ToString());
                // linha 1 coluna 2
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[1].Cells[2].Value.ToString()) == 1) acertos++;
                }
               

                //quadro 6
                somatoria = 0;
                for (y = 876; y <= 1194; y++)
                {
                    for (x = 1362; x < 1714; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

               // MessageBox.Show(dataGridView1.Rows[1].Cells[3].Value.ToString());
                // linha 1 coluna 3
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[1].Cells[3].Value.ToString()) == 1) acertos++;
                }

                //quadro 7
                somatoria = 0;
                for (y = 1504; y <= 1817; y++)
                {
                    for (x = 99; x < 493; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[2].Cells[1].Value.ToString());
                // linha 2 coluna 1
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value.ToString()) == 1) acertos++;
                }

                //quadro 8
                somatoria = 0;
                for (y = 1504; y <= 1817; y++)
                {
                    for (x = 753; x < 1082; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[2].Cells[2].Value.ToString());
                // linha 2 coluna 2
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[2].Cells[2].Value.ToString()) == 1) acertos++;
                }

                //quadro 9
                somatoria = 0;
                for (y = 1504; y <= 1817; y++)
                {
                    for (x = 1362; x < 1714; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[2].Cells[3].Value.ToString());
                // linha 2 coluna 3
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[2].Cells[3].Value.ToString()) == 1) acertos++;
                }
               

                //quadro 10
                somatoria = 0;
                for (y = 2111; y <= 2439; y++)
                {
                    for (x = 99; x < 493; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[3].Cells[1].Value.ToString());
                // linha 3 coluna 1
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[3].Cells[1].Value.ToString()) == 1) acertos++;
                }
               

                //quadro 11
                somatoria = 0;
                for (y = 2111; y <= 2439; y++)
                {
                    for (x = 753; x < 1082; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[3].Cells[2].Value.ToString());
                // linha 3 coluna 2
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[3].Cells[2].Value.ToString()) == 1) acertos++;
                }

                //quadro 12
                somatoria = 0;
                for (y = 2111; y <= 2439; y++)
                {
                    for (x = 1362; x < 1714; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[3].Cells[3].Value.ToString());
                // linha 3 coluna 3
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[3].Cells[3].Value.ToString()) == 1) acertos++;
                }
               
                //quadro 13
                somatoria = 0;
                for (y = 2709; y <= 3009; y++)
                {
                    for (x = 99; x < 493; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[4].Cells[1].Value.ToString());
                // linha 4 coluna 1
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[4].Cells[1].Value.ToString()) == 1) acertos++;
                }
               

                //quadro 14
                somatoria = 0;
                for (y = 2709; y <= 3009; y++)
                {
                    for (x = 753; x < 1082; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[4].Cells[2].Value.ToString());
                // linha 4 coluna 2
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[4].Cells[2].Value.ToString()) == 1) acertos++;
                }
              

                //quadro 15
                somatoria = 0;
                for (y = 2709; y <= 3009; y++)
                {
                    for (x = 1362; x < 1714; x++)
                    {
                        // obtém pixel da linha e coluna da imagem		
                        Color pixelColor = image1.GetPixel(x, y);
                        //Detecta se o pixel é preto
                        if ((pixelColor.R == 0) && (pixelColor.G == 0) && (pixelColor.B == 0))
                        {
                            //Faz a troca da cor preta para a vermelha
                            pixelColor = Color.FromArgb(255, 0, 0);
                            somatoria++;
                        }
                        image1.SetPixel(x, y, pixelColor);
                    }
                }

                //MessageBox.Show(dataGridView1.Rows[4].Cells[3].Value.ToString());
                // linha 4 coluna 3
                if (somatoria > valorReconhece)// é preto então marcado pelo usuário
                {
                    // analisa se foi selecionado no gabarito
                    if (Convert.ToInt32(dataGridView1.Rows[4].Cells[3].Value.ToString()) == 1) acertos++;
                }


                //Soma os acertos e exibe em uma caixa de dialogo
                MessageBox.Show("Acertos: " + acertos);
                //Coloca a imagem a caixa de imagem
                pictureBox1.Image = image1;
                //Desenhas os quadrados na imagem
                pictureBox1.Refresh();

            }
            //Erro ao nao informar nenhum valor
            catch (NullReferenceException)
            {
                //Exibe uma caixa de mensagem
                MessageBox.Show("Preencha a tabela com 0 ou 1");
                //Limpa as colunas da tabela
                dataGridView1.Columns.Clear();
                //Limpa as linhas da tabela
                dataGridView1.Rows.Clear();
                //Metodo para carregar os itens da tabela
                carregaTabela();
            }
            //Erro ao colocar letras na tabela
            catch (FormatException)
            {
                //Exibe uma caixa de mensagem
                MessageBox.Show("Digite apenas:\n1 - Para questões corretas\n0 - Para questões erradas");
                //Limpa as colunas da tabela
                dataGridView1.Columns.Clear();
                //Limpa as linhas da tabela
                dataGridView1.Rows.Clear();
                //Metodo para carregar os itens da tabela
                carregaTabela();
            }
        }

        public void carregaTabela()
        {
            //Cria a coluna das questoes
            dataGridView1.Columns.Add("Questão", "Questão");
            //Cria a coluna A
            dataGridView1.Columns.Add("A", "A");
            //Cria a coluna B
            dataGridView1.Columns.Add("B", "B");
            //Cria a coluna C
            dataGridView1.Columns.Add("C", "C");
            //Ajudas o tamanho das colunas 
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //Percorre as lihas
            for (int j = 0; j <=4; j++)
            //Percorre as celulas
            for (int i = 1; i <=3; i++)
            {
                //Cria uma linha
                dataGridView1.Rows.Add();
                //Adiciona o valor 0 a essa linha
                dataGridView1.Rows[j].Cells[i].Value = 0;

            }
            //Adiciona a primeira colunas os valores de 1 a 5
            for (int j = 0; j <= 4; j++) dataGridView1.Rows[j].Cells[0].Value = j+1;
        }


    }
}
