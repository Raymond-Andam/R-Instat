﻿' R- Instat
' Copyright (C) 2015-2017
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License 
' along with this program.  If not, see <http://www.gnu.org/licenses/>.

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgViewObjects
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblSelectedObject = New System.Windows.Forms.Label()
        Me.rdoStructure = New System.Windows.Forms.RadioButton()
        Me.rdoAllContents = New System.Windows.Forms.RadioButton()
        Me.rdoComponent = New System.Windows.Forms.RadioButton()
        Me.rdoPrint = New System.Windows.Forms.RadioButton()
        Me.ucrReceiverSelectedObject = New instat.ucrReceiverSingle()
        Me.ucrSelectorForViewObject = New instat.ucrSelectorByDataFrameAddRemove()
        Me.ucrBase = New instat.ucrButtons()
        Me.ucrPnlContentsToView = New instat.UcrPanel()
        Me.ucrInputObjectType = New instat.ucrInputComboBox()
        Me.lblObjectType = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblSelectedObject
        '
        Me.lblSelectedObject.AutoSize = True
        Me.lblSelectedObject.Location = New System.Drawing.Point(250, 72)
        Me.lblSelectedObject.Name = "lblSelectedObject"
        Me.lblSelectedObject.Size = New System.Drawing.Size(41, 13)
        Me.lblSelectedObject.TabIndex = 1
        Me.lblSelectedObject.Text = "Object:"
        '
        'rdoStructure
        '
        Me.rdoStructure.AutoSize = True
        Me.rdoStructure.Location = New System.Drawing.Point(256, 144)
        Me.rdoStructure.Name = "rdoStructure"
        Me.rdoStructure.Size = New System.Drawing.Size(68, 17)
        Me.rdoStructure.TabIndex = 5
        Me.rdoStructure.TabStop = True
        Me.rdoStructure.Tag = "Structure"
        Me.rdoStructure.Text = "Structure"
        Me.rdoStructure.UseVisualStyleBackColor = True
        '
        'rdoAllContents
        '
        Me.rdoAllContents.AutoSize = True
        Me.rdoAllContents.Location = New System.Drawing.Point(256, 166)
        Me.rdoAllContents.Name = "rdoAllContents"
        Me.rdoAllContents.Size = New System.Drawing.Size(81, 17)
        Me.rdoAllContents.TabIndex = 6
        Me.rdoAllContents.TabStop = True
        Me.rdoAllContents.Tag = "All_Contents"
        Me.rdoAllContents.Text = "All Contents"
        Me.rdoAllContents.UseVisualStyleBackColor = True
        '
        'rdoComponent
        '
        Me.rdoComponent.AutoSize = True
        Me.rdoComponent.Location = New System.Drawing.Point(256, 188)
        Me.rdoComponent.Name = "rdoComponent"
        Me.rdoComponent.Size = New System.Drawing.Size(79, 17)
        Me.rdoComponent.TabIndex = 7
        Me.rdoComponent.TabStop = True
        Me.rdoComponent.Tag = "Component"
        Me.rdoComponent.Text = "Component"
        Me.rdoComponent.UseVisualStyleBackColor = True
        '
        'rdoPrint
        '
        Me.rdoPrint.AutoSize = True
        Me.rdoPrint.Location = New System.Drawing.Point(256, 122)
        Me.rdoPrint.Name = "rdoPrint"
        Me.rdoPrint.Size = New System.Drawing.Size(46, 17)
        Me.rdoPrint.TabIndex = 4
        Me.rdoPrint.TabStop = True
        Me.rdoPrint.Text = "Print"
        Me.rdoPrint.UseVisualStyleBackColor = True
        '
        'ucrReceiverSelectedObject
        '
        Me.ucrReceiverSelectedObject.AutoSize = True
        Me.ucrReceiverSelectedObject.frmParent = Me
        Me.ucrReceiverSelectedObject.Location = New System.Drawing.Point(250, 87)
        Me.ucrReceiverSelectedObject.Margin = New System.Windows.Forms.Padding(0)
        Me.ucrReceiverSelectedObject.Name = "ucrReceiverSelectedObject"
        Me.ucrReceiverSelectedObject.Selector = Nothing
        Me.ucrReceiverSelectedObject.Size = New System.Drawing.Size(137, 20)
        Me.ucrReceiverSelectedObject.strNcFilePath = ""
        Me.ucrReceiverSelectedObject.TabIndex = 2
        Me.ucrReceiverSelectedObject.ucrSelector = Nothing
        '
        'ucrSelectorForViewObject
        '
        Me.ucrSelectorForViewObject.AutoSize = True
        Me.ucrSelectorForViewObject.bDropUnusedFilterLevels = False
        Me.ucrSelectorForViewObject.bShowHiddenColumns = False
        Me.ucrSelectorForViewObject.bUseCurrentFilter = True
        Me.ucrSelectorForViewObject.Location = New System.Drawing.Point(10, 10)
        Me.ucrSelectorForViewObject.Margin = New System.Windows.Forms.Padding(0)
        Me.ucrSelectorForViewObject.Name = "ucrSelectorForViewObject"
        Me.ucrSelectorForViewObject.Size = New System.Drawing.Size(213, 183)
        Me.ucrSelectorForViewObject.TabIndex = 0
        '
        'ucrBase
        '
        Me.ucrBase.AutoSize = True
        Me.ucrBase.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ucrBase.Location = New System.Drawing.Point(8, 222)
        Me.ucrBase.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ucrBase.Name = "ucrBase"
        Me.ucrBase.Size = New System.Drawing.Size(408, 52)
        Me.ucrBase.TabIndex = 8
        '
        'ucrPnlContentsToView
        '
        Me.ucrPnlContentsToView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ucrPnlContentsToView.Location = New System.Drawing.Point(250, 113)
        Me.ucrPnlContentsToView.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.ucrPnlContentsToView.Name = "ucrPnlContentsToView"
        Me.ucrPnlContentsToView.Size = New System.Drawing.Size(120, 100)
        Me.ucrPnlContentsToView.TabIndex = 3
        '
        'ucrInputObjectType
        '
        Me.ucrInputObjectType.AddQuotesIfUnrecognised = True
        Me.ucrInputObjectType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ucrInputObjectType.GetSetSelectedIndex = -1
        Me.ucrInputObjectType.IsReadOnly = False
        Me.ucrInputObjectType.Location = New System.Drawing.Point(250, 47)
        Me.ucrInputObjectType.Name = "ucrInputObjectType"
        Me.ucrInputObjectType.Size = New System.Drawing.Size(137, 21)
        Me.ucrInputObjectType.TabIndex = 9
        '
        'lblObjectType
        '
        Me.lblObjectType.AutoSize = True
        Me.lblObjectType.Location = New System.Drawing.Point(250, 32)
        Me.lblObjectType.Name = "lblObjectType"
        Me.lblObjectType.Size = New System.Drawing.Size(106, 13)
        Me.lblObjectType.TabIndex = 10
        Me.lblObjectType.Text = "Object Type to View:"
        '
        'dlgViewObjects
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(416, 277)
        Me.Controls.Add(Me.lblObjectType)
        Me.Controls.Add(Me.ucrInputObjectType)
        Me.Controls.Add(Me.rdoPrint)
        Me.Controls.Add(Me.rdoComponent)
        Me.Controls.Add(Me.rdoAllContents)
        Me.Controls.Add(Me.rdoStructure)
        Me.Controls.Add(Me.ucrReceiverSelectedObject)
        Me.Controls.Add(Me.lblSelectedObject)
        Me.Controls.Add(Me.ucrSelectorForViewObject)
        Me.Controls.Add(Me.ucrBase)
        Me.Controls.Add(Me.ucrPnlContentsToView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgViewObjects"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = ""
        Me.Text = "View Object"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ucrBase As ucrButtons
    Friend WithEvents ucrSelectorForViewObject As ucrSelectorByDataFrameAddRemove
    Friend WithEvents lblSelectedObject As Label
    Friend WithEvents ucrReceiverSelectedObject As ucrReceiverSingle
    Friend WithEvents rdoStructure As RadioButton
    Friend WithEvents rdoAllContents As RadioButton
    Friend WithEvents rdoComponent As RadioButton
    Friend WithEvents rdoPrint As RadioButton
    Friend WithEvents ucrPnlContentsToView As UcrPanel
    Friend WithEvents lblObjectType As Label
    Friend WithEvents ucrInputObjectType As ucrInputComboBox
End Class
