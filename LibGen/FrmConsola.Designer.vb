<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmConsola
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmConsola))
        Me.RichConsole1 = New WindowsApplication1.RichConsole()
        Me.SuspendLayout()
        '
        'RichConsole1
        '
        Me.RichConsole1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.RichConsole1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichConsole1.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichConsole1.ForeColor = System.Drawing.Color.LightGray
        Me.RichConsole1.Location = New System.Drawing.Point(0, 0)
        Me.RichConsole1.Name = "RichConsole1"
        Me.RichConsole1.Size = New System.Drawing.Size(645, 440)
        Me.RichConsole1.TabIndex = 0
        Me.RichConsole1.Text = ""
        '
        'FrmConsola
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(645, 440)
        Me.Controls.Add(Me.RichConsole1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmConsola"
        Me.Text = "NOVA"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RichConsole1 As RichConsole
End Class
