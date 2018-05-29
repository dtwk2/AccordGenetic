// Approximation (Symbolic Regression) using Genetic Programming and Gene Expression Programming
// AForge.NET framework
// http://www.aforgenet.com/framework/
//
// Copyright � AForge.NET, 2006-2011
// contacts@aforgenet.com
//

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;

using AForge;
using Accord.Genetic;
using Accord.Controls;
using Accord;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccordGenetic.Wrapper;

namespace SampleApp
{

    public class Approximation : GeneticAlgorithmForm
    {

        private int functionsSet = 0;
        private int geneticMethod = 0;


        #region GUI

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView dataList;
        private System.Windows.Forms.ColumnHeader xColumnHeader;
        private System.Windows.Forms.ColumnHeader yColumnHeader;
        private System.Windows.Forms.Button loadDataButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.GroupBox groupBox2;
        private Accord.Controls.Chart chart;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox populationSizeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox selectionBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox iterationsBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox currentIterationBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox currentErrorBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox functionsSetBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox geneticMethodBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox solutionBox;




        public Approximation()
        {
            InitializeComponent();

            chart.AddDataSeries("data", Color.Red, Chart.SeriesType.Dots, 5);
            chart.AddDataSeries("solution", Color.Blue, Chart.SeriesType.Line, 1);

            selectionBox.SelectedIndex = selectionMethod;
            functionsSetBox.SelectedIndex = functionsSet;
            geneticMethodBox.SelectedIndex = geneticMethod;
            UpdateSettings();

            openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Sample data (approximation)");
        }



        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataList = new System.Windows.Forms.ListView();
            this.xColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.yColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.loadDataButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chart = new Accord.Controls.Chart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.geneticMethodBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.functionsSetBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.iterationsBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.selectionBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.populationSizeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.currentErrorBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.currentIterationBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.solutionBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 

            this.startButton.Location = new System.Drawing.Point(848, 442);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(992, 442);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataList);
            this.groupBox1.Controls.Add(this.loadDataButton);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 453);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // dataList
            // 
            this.dataList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xColumnHeader,
            this.yColumnHeader});
            this.dataList.FullRowSelect = true;
            this.dataList.GridLines = true;
            this.dataList.Location = new System.Drawing.Point(16, 29);
            this.dataList.Name = "dataList";
            this.dataList.Size = new System.Drawing.Size(256, 373);
            this.dataList.TabIndex = 0;
            this.dataList.UseCompatibleStateImageBehavior = false;
            this.dataList.View = System.Windows.Forms.View.Details;
            // 
            // xColumnHeader
            // 
            this.xColumnHeader.Text = "X";
            // 
            // yColumnHeader
            // 
            this.yColumnHeader.Text = "Y";
            // 
            // loadDataButton
            // 
            this.loadDataButton.Location = new System.Drawing.Point(16, 409);
            this.loadDataButton.Name = "loadDataButton";
            this.loadDataButton.Size = new System.Drawing.Size(120, 34);
            this.loadDataButton.TabIndex = 1;
            this.loadDataButton.Text = "&Load";
            this.loadDataButton.Click += new System.EventHandler(this.loadDataButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "CSV (Comma delimited) (*.csv)|*.csv";
            this.openFileDialog.Title = "Select data file";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chart);
            this.groupBox2.Location = new System.Drawing.Point(320, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 453);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Function";
            // 
            // chart
            // 
            this.chart.Location = new System.Drawing.Point(16, 29);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(448, 409);
            this.chart.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.geneticMethodBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.functionsSetBox);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.iterationsBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.selectionBox);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.populationSizeBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(816, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(296, 289);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Settings";
            // 
            // geneticMethodBox
            // 
            this.geneticMethodBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.geneticMethodBox.Items.AddRange(new object[] {
            "GP",
            "GEP"});
            this.geneticMethodBox.Location = new System.Drawing.Point(176, 139);
            this.geneticMethodBox.Name = "geneticMethodBox";
            this.geneticMethodBox.Size = new System.Drawing.Size(104, 28);
            this.geneticMethodBox.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 23);
            this.label8.TabIndex = 6;
            this.label8.Text = "Genetic method:";
            // 
            // functionsSetBox
            // 
            this.functionsSetBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.functionsSetBox.Items.AddRange(new object[] {
            "Simple",
            "Extended"});
            this.functionsSetBox.Location = new System.Drawing.Point(176, 102);
            this.functionsSetBox.Name = "functionsSetBox";
            this.functionsSetBox.Size = new System.Drawing.Size(104, 28);
            this.functionsSetBox.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 24);
            this.label7.TabIndex = 4;
            this.label7.Text = "Functions set:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(200, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 22);
            this.label4.TabIndex = 10;
            this.label4.Text = "( 0 - inifinity )";
            // 
            // iterationsBox
            // 
            this.iterationsBox.Location = new System.Drawing.Point(200, 227);
            this.iterationsBox.Name = "iterationsBox";
            this.iterationsBox.Size = new System.Drawing.Size(80, 26);
            this.iterationsBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Iterations:";
            // 
            // selectionBox
            // 
            this.selectionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectionBox.Items.AddRange(new object[] {
            "Elite",
            "Rank",
            "Roulette"});
            this.selectionBox.Location = new System.Drawing.Point(176, 66);
            this.selectionBox.Name = "selectionBox";
            this.selectionBox.Size = new System.Drawing.Size(104, 28);
            this.selectionBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Selection method:";
            // 
            // populationSizeBox
            // 
            this.populationSizeBox.Location = new System.Drawing.Point(200, 29);
            this.populationSizeBox.Name = "populationSizeBox";
            this.populationSizeBox.Size = new System.Drawing.Size(80, 26);
            this.populationSizeBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Population size:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.currentErrorBox);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.currentIterationBox);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(816, 316);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(296, 109);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Current iteration";
            // 
            // currentErrorBox
            // 
            this.currentErrorBox.Location = new System.Drawing.Point(200, 66);
            this.currentErrorBox.Name = "currentErrorBox";
            this.currentErrorBox.ReadOnly = true;
            this.currentErrorBox.Size = new System.Drawing.Size(80, 26);
            this.currentErrorBox.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 23);
            this.label6.TabIndex = 2;
            this.label6.Text = "Error:";
            // 
            // currentIterationBox
            // 
            this.currentIterationBox.Location = new System.Drawing.Point(200, 29);
            this.currentIterationBox.Name = "currentIterationBox";
            this.currentIterationBox.ReadOnly = true;
            this.currentIterationBox.Size = new System.Drawing.Size(80, 26);
            this.currentIterationBox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 24);
            this.label5.TabIndex = 0;
            this.label5.Text = "Iteration:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.solutionBox);
            this.groupBox5.Location = new System.Drawing.Point(16, 482);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1096, 73);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Solution:";
            // 
            // solutionBox
            // 
            this.solutionBox.Location = new System.Drawing.Point(16, 29);
            this.solutionBox.Name = "solutionBox";
            this.solutionBox.ReadOnly = true;
            this.solutionBox.Size = new System.Drawing.Size(1064, 26);
            this.solutionBox.TabIndex = 0;
            // 
            // Approximation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.ClientSize = new System.Drawing.Size(1145, 581);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Approximation";
            this.Text = "Approximation (Symbolic Regression) using Genetic Programming and Gene Expression" +
    " Programming";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.groupBox5, 0);
            this.Controls.SetChildIndex(this.startButton, 0);
            this.Controls.SetChildIndex(this.stopButton, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
       
        }
        #endregion







        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            bool isDisposed = true;
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        // Update settings controls
        private void UpdateSettings()
        {
            populationSizeBox.Text = populationSize.ToString();
            iterationsBox.Text = iterations.ToString();
        }

        // Load data
        private void loadDataButton_Click(object sender, System.EventArgs e)
        {
            // show file selection dialog
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = null;
                // read maximum 50 points
                float[,] tempData = new float[50, 2];
                float minX = float.MaxValue;
                float maxX = float.MinValue;

                try
                {
                    // open selected file
                    reader = File.OpenText(openFileDialog.FileName);
                    string str = null;
                    int i = 0;

                    // read the data
                    while ((i < 50) && ((str = reader.ReadLine()) != null))
                    {
                        string[] strs = str.Split(';');
                        if (strs.Length == 1)
                            strs = str.Split(',');
                        // parse X
                        tempData[i, 0] = float.Parse(strs[0]);
                        tempData[i, 1] = float.Parse(strs[1]);

                        // search for min value
                        if (tempData[i, 0] < minX)
                            minX = tempData[i, 0];
                        // search for max value
                        if (tempData[i, 0] > maxX)
                            maxX = tempData[i, 0];

                        i++;
                    }

                    // allocate and set data
                    dataToShow = new double[i, 2];
                    Array.Copy(tempData, 0, dataToShow, 0, i * 2);
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed reading the file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    // close file
                    if (reader != null)
                        reader.Close();
                }

                // update list and chart
                UpdateDataListView();
                chart.RangeX = new Range(minX, maxX);
                chart.UpdateDataSeries("data", dataToShow);
                chart.UpdateDataSeries("solution", null);
                // enable "Start" button
                startButton.Enabled = true;
            }
        }

        // Update data in list view
        private void UpdateDataListView()
        {
            // remove all current records
            dataList.Items.Clear();
            // add new records
            for (int i = 0, n = dataToShow.GetLength(0); i < n; i++)
            {
                dataList.Items.Add(dataToShow[i, 0].ToString());
                dataList.Items[i].SubItems.Add(dataToShow[i, 1].ToString());
            }
        }

        // Delegates to enable async calls for setting controls properties
        private delegate void EnableCallback(bool enable);

        // Enable/disale controls (safe for threading)
        private void EnableControls(bool enable)
        {
            if (InvokeRequired)
            {
                EnableCallback d = new EnableCallback(EnableControls);
                Invoke(d, new object[] { enable });
            }

            else
            {
                loadDataButton.Enabled = enable;
                populationSizeBox.Enabled = enable;
                iterationsBox.Enabled = enable;
                selectionBox.Enabled = enable;
                functionsSetBox.Enabled = enable;
                geneticMethodBox.Enabled = enable;

                startButton.Enabled = enable;
                stopButton.Enabled = !enable;
            }
        }



        #endregion GUI


        // On button "Start"
        protected override void startButton_Click(object sender, System.EventArgs e)
        {
            EnableControls(false);
            solutionBox.Text = string.Empty;

            AccordGenetic.Wrapper.ApproximationWrap wrap = new AccordGenetic.Wrapper.ApproximationWrap(
               data: dataToShow, 
              functionsSet: functionsSetBox.SelectedIndex,
               populationSize: int.TryParse(populationSizeBox.Text, out int result1) ? Math.Max(10, Math.Min(100, result1)) : 40, 
               geneticMethod: geneticMethodBox.SelectedIndex, 
              selectionMethod: selectionBox.SelectedIndex, 
              minRange:  chart.RangeX.Min,
              lengthRange:  chart.RangeX.Length);

   
            SearchSolution(wrap);


        }


        // Worker thread
        void SearchSolution(AccordGenetic.Wrapper.ApproximationWrap wrap)
        {
            iterations = int.TryParse(iterationsBox.Text, out int result2) ? Math.Max(1, result2) : 100;

            var progressHandler = new Progress<KeyValuePair<int, AccordGenetic.Wrapper.Result>>(kvp => ProgressUpdate(kvp, wrap));

            cts = new CancellationTokenSource();

            Task tsk = Task.Run(() => wrap.RunMultipleEpochs(iterations, cts.Token, progressHandler));
            tsk.ContinueWith(
               t =>
               {
                   // faulted with exception
                   if (t.IsFaulted)
                   {

                       Exception ex = t.Exception;
                       while (ex is AggregateException && ex.InnerException != null)
                           ex = ex.InnerException;
                       MessageBox.Show("Error: " + ex.Message);
                   }
                   else if (t.IsCanceled)
                   {
                       //MessageBox.Show("Cancelled");
                   }
                   else
                   {
                       if (!cts.IsCancellationRequested)
                       { /* final update*/}
        
                   }
                   // completed successfully/ check if closed button has been clicked
                   if (!IsClosed)
                   {
                       EnableControls(true);
                   }
  
               });


        }



        public void ProgressUpdate(KeyValuePair<int, AccordGenetic.Wrapper.Result> kvp, ApproximationWrap wrap)
        {
            // update info
        
            var error = wrap.EvaluateError();
            chart.UpdateDataSeries("solution", kvp.Value.Output);
            SetText(currentIterationBox, kvp.Key.ToString());

            SetText(currentErrorBox, error.Prediction.ToString("F3"));
            SetText(solutionBox, kvp.Value.BestSolution.ToString());
        }
    }
}
