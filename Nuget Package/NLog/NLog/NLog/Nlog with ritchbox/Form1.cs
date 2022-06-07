using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nlog_with_ritchbox
{
    public partial class Form1 : Form
    {
        Log log =null;
        public Form1()
        {
            InitializeComponent();            
        }      

        private void button1_Click(object sender, EventArgs e)
        {
            log.logger.Trace("trace log message");
            log.logger.Debug("debug log message");
            log.logger.Info("info log message");
            log.logger.Warn("warn log message");
            log.logger.Error("error log message");
            log.logger.Fatal("fatal log message");
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            log = new Log("");

        }
    }
   
}
