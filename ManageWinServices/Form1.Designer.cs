namespace ManageWinServicese
{
    partial class RdpServicesStatus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.servicesStatusDetailsGrid = new System.Windows.Forms.DataGridView();
            this.txtServerNameValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnStopService = new System.Windows.Forms.Button();
            this.btnStartService = new System.Windows.Forms.Button();
            this.btnStartAll = new System.Windows.Forms.Button();
            this.btnStopAll = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.servicesStatusDetailsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerNameValidator)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(141, 25);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(349, 22);
            this.txtServer.TabIndex = 1;
            this.txtServer.Validating += new System.ComponentModel.CancelEventHandler(this.txtServer_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Server Name";
            // 
            // servicesStatusDetailsGrid
            // 
            this.servicesStatusDetailsGrid.AllowUserToAddRows = false;
            this.servicesStatusDetailsGrid.AllowUserToDeleteRows = false;
            this.servicesStatusDetailsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.servicesStatusDetailsGrid.Location = new System.Drawing.Point(4, 63);
            this.servicesStatusDetailsGrid.Name = "servicesStatusDetailsGrid";
            this.servicesStatusDetailsGrid.ReadOnly = true;
            this.servicesStatusDetailsGrid.RowTemplate.Height = 24;
            this.servicesStatusDetailsGrid.Size = new System.Drawing.Size(646, 647);
            this.servicesStatusDetailsGrid.TabIndex = 9;
            // 
            // txtServerNameValidator
            // 
            this.txtServerNameValidator.ContainerControl = this;
            // 
            // btnStopService
            // 
            this.btnStopService.Location = new System.Drawing.Point(8, 73);
            this.btnStopService.Name = "btnStopService";
            this.btnStopService.Size = new System.Drawing.Size(115, 32);
            this.btnStopService.TabIndex = 10;
            this.btnStopService.Text = "Stop Service";
            this.btnStopService.UseVisualStyleBackColor = true;
            this.btnStopService.Click += new System.EventHandler(this.btnStopService_Click);
            // 
            // btnStartService
            // 
            this.btnStartService.Location = new System.Drawing.Point(6, 30);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(115, 31);
            this.btnStartService.TabIndex = 11;
            this.btnStartService.Text = "Start Service";
            this.btnStartService.UseVisualStyleBackColor = true;
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            // 
            // btnStartAll
            // 
            this.btnStartAll.Location = new System.Drawing.Point(6, 21);
            this.btnStartAll.Name = "btnStartAll";
            this.btnStartAll.Size = new System.Drawing.Size(115, 32);
            this.btnStartAll.TabIndex = 12;
            this.btnStartAll.Text = "Start All";
            this.btnStartAll.UseVisualStyleBackColor = true;
            this.btnStartAll.Click += new System.EventHandler(this.btnStartAll_Click);
            // 
            // btnStopAll
            // 
            this.btnStopAll.Location = new System.Drawing.Point(6, 62);
            this.btnStopAll.Name = "btnStopAll";
            this.btnStopAll.Size = new System.Drawing.Size(115, 32);
            this.btnStopAll.TabIndex = 13;
            this.btnStopAll.Text = "Stop All";
            this.btnStopAll.UseVisualStyleBackColor = true;
            this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.Location = new System.Drawing.Point(520, 19);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(130, 35);
            this.btnGetAll.TabIndex = 14;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = true;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStopService);
            this.groupBox1.Controls.Add(this.btnStartService);
            this.groupBox1.Location = new System.Drawing.Point(656, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 114);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "By Selection";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStartAll);
            this.groupBox2.Controls.Add(this.btnStopAll);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(656, 199);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(159, 115);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "All";
            // 
            // RdpServicesStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 790);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGetAll);
            this.Controls.Add(this.servicesStatusDetailsGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServer);
            this.Name = "RdpServicesStatus";
            this.Text = "RDP Services Status";
            this.Load += new System.EventHandler(this.RdpServicesStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.servicesStatusDetailsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerNameValidator)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView servicesStatusDetailsGrid;
        private System.Windows.Forms.ErrorProvider txtServerNameValidator;
        private System.Windows.Forms.Button btnStopService;
        private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.Button btnStopAll;
        private System.Windows.Forms.Button btnStartAll;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

