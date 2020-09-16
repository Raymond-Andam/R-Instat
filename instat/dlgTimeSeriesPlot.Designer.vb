﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgTimeSeriesPlot
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
        Me.ucrBase = New instat.ucrButtons()
        Me.ucrSelectorTimeSeriesPlots = New instat.ucrSelectorByDataFrameAddRemove()
        Me.lblReferenceValues = New System.Windows.Forms.Label()
        Me.ucrReceiverReference = New instat.ucrReceiverSingle()
        Me.ucrReceiverEstimates = New instat.ucrReceiverSingle()
        Me.lblEstimates = New System.Windows.Forms.Label()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.ucrReceiverTime = New instat.ucrReceiverSingle()
        Me.lblStation = New System.Windows.Forms.Label()
        Me.ucrReceiverStation = New instat.ucrReceiverSingle()
        Me.ucrChkNAValues = New instat.ucrCheck()
        Me.ucrChkIncludeMeanLines = New instat.ucrCheck()
        Me.ucrChkIncludePoints = New instat.ucrCheck()
        Me.cmdSummaries = New System.Windows.Forms.Button()
        Me.ucrSavePlot = New instat.ucrSaveGraph()
        Me.SuspendLayout()
        '
        'ucrBase
        '
        Me.ucrBase.Location = New System.Drawing.Point(10, 428)
        Me.ucrBase.Name = "ucrBase"
        Me.ucrBase.Size = New System.Drawing.Size(410, 52)
        Me.ucrBase.TabIndex = 14
        '
        'ucrSelectorTimeSeriesPlots
        '
        Me.ucrSelectorTimeSeriesPlots.bDropUnusedFilterLevels = False
        Me.ucrSelectorTimeSeriesPlots.bShowHiddenColumns = False
        Me.ucrSelectorTimeSeriesPlots.bUseCurrentFilter = True
        Me.ucrSelectorTimeSeriesPlots.Location = New System.Drawing.Point(12, 9)
        Me.ucrSelectorTimeSeriesPlots.Margin = New System.Windows.Forms.Padding(0)
        Me.ucrSelectorTimeSeriesPlots.Name = "ucrSelectorTimeSeriesPlots"
        Me.ucrSelectorTimeSeriesPlots.Size = New System.Drawing.Size(218, 180)
        Me.ucrSelectorTimeSeriesPlots.TabIndex = 0
        '
        'lblReferenceValues
        '
        Me.lblReferenceValues.AutoSize = True
        Me.lblReferenceValues.Location = New System.Drawing.Point(286, 24)
        Me.lblReferenceValues.Name = "lblReferenceValues"
        Me.lblReferenceValues.Size = New System.Drawing.Size(95, 13)
        Me.lblReferenceValues.TabIndex = 1
        Me.lblReferenceValues.Text = "Reference Values:"
        '
        'ucrReceiverReference
        '
        Me.ucrReceiverReference.frmParent = Me
        Me.ucrReceiverReference.Location = New System.Drawing.Point(286, 39)
        Me.ucrReceiverReference.Margin = New System.Windows.Forms.Padding(0)
        Me.ucrReceiverReference.Name = "ucrReceiverReference"
        Me.ucrReceiverReference.Selector = Nothing
        Me.ucrReceiverReference.Size = New System.Drawing.Size(120, 20)
        Me.ucrReceiverReference.strNcFilePath = ""
        Me.ucrReceiverReference.TabIndex = 2
        Me.ucrReceiverReference.ucrSelector = Nothing
        '
        'ucrReceiverEstimates
        '
        Me.ucrReceiverEstimates.frmParent = Me
        Me.ucrReceiverEstimates.Location = New System.Drawing.Point(286, 86)
        Me.ucrReceiverEstimates.Margin = New System.Windows.Forms.Padding(0)
        Me.ucrReceiverEstimates.Name = "ucrReceiverEstimates"
        Me.ucrReceiverEstimates.Selector = Nothing
        Me.ucrReceiverEstimates.Size = New System.Drawing.Size(120, 20)
        Me.ucrReceiverEstimates.strNcFilePath = ""
        Me.ucrReceiverEstimates.TabIndex = 4
        Me.ucrReceiverEstimates.ucrSelector = Nothing
        '
        'lblEstimates
        '
        Me.lblEstimates.AutoSize = True
        Me.lblEstimates.Location = New System.Drawing.Point(286, 71)
        Me.lblEstimates.Name = "lblEstimates"
        Me.lblEstimates.Size = New System.Drawing.Size(55, 13)
        Me.lblEstimates.TabIndex = 3
        Me.lblEstimates.Text = "Estimates:"
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Location = New System.Drawing.Point(286, 118)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(70, 13)
        Me.lblTime.TabIndex = 5
        Me.lblTime.Text = "Time (X axis):"
        '
        'ucrReceiverTime
        '
        Me.ucrReceiverTime.frmParent = Me
        Me.ucrReceiverTime.Location = New System.Drawing.Point(286, 133)
        Me.ucrReceiverTime.Margin = New System.Windows.Forms.Padding(0)
        Me.ucrReceiverTime.Name = "ucrReceiverTime"
        Me.ucrReceiverTime.Selector = Nothing
        Me.ucrReceiverTime.Size = New System.Drawing.Size(120, 20)
        Me.ucrReceiverTime.strNcFilePath = ""
        Me.ucrReceiverTime.TabIndex = 6
        Me.ucrReceiverTime.ucrSelector = Nothing
        '
        'lblStation
        '
        Me.lblStation.AutoSize = True
        Me.lblStation.Location = New System.Drawing.Point(286, 165)
        Me.lblStation.Name = "lblStation"
        Me.lblStation.Size = New System.Drawing.Size(43, 13)
        Me.lblStation.TabIndex = 7
        Me.lblStation.Text = "Station:"
        '
        'ucrReceiverStation
        '
        Me.ucrReceiverStation.frmParent = Me
        Me.ucrReceiverStation.Location = New System.Drawing.Point(286, 180)
        Me.ucrReceiverStation.Margin = New System.Windows.Forms.Padding(0)
        Me.ucrReceiverStation.Name = "ucrReceiverStation"
        Me.ucrReceiverStation.Selector = Nothing
        Me.ucrReceiverStation.Size = New System.Drawing.Size(120, 20)
        Me.ucrReceiverStation.strNcFilePath = ""
        Me.ucrReceiverStation.TabIndex = 8
        Me.ucrReceiverStation.ucrSelector = Nothing
        '
        'ucrChkNAValues
        '
        Me.ucrChkNAValues.Checked = False
        Me.ucrChkNAValues.Location = New System.Drawing.Point(10, 242)
        Me.ucrChkNAValues.Name = "ucrChkNAValues"
        Me.ucrChkNAValues.Size = New System.Drawing.Size(100, 20)
        Me.ucrChkNAValues.TabIndex = 9
        '
        'ucrChkIncludeMeanLines
        '
        Me.ucrChkIncludeMeanLines.Checked = False
        Me.ucrChkIncludeMeanLines.Location = New System.Drawing.Point(10, 316)
        Me.ucrChkIncludeMeanLines.Name = "ucrChkIncludeMeanLines"
        Me.ucrChkIncludeMeanLines.Size = New System.Drawing.Size(100, 20)
        Me.ucrChkIncludeMeanLines.TabIndex = 11
        '
        'ucrChkIncludePoints
        '
        Me.ucrChkIncludePoints.Checked = False
        Me.ucrChkIncludePoints.Location = New System.Drawing.Point(10, 279)
        Me.ucrChkIncludePoints.Name = "ucrChkIncludePoints"
        Me.ucrChkIncludePoints.Size = New System.Drawing.Size(100, 20)
        Me.ucrChkIncludePoints.TabIndex = 10
        '
        'cmdSummaries
        '
        Me.cmdSummaries.Location = New System.Drawing.Point(9, 345)
        Me.cmdSummaries.Name = "cmdSummaries"
        Me.cmdSummaries.Size = New System.Drawing.Size(75, 23)
        Me.cmdSummaries.TabIndex = 12
        Me.cmdSummaries.Text = "Summaries"
        Me.cmdSummaries.UseVisualStyleBackColor = True
        '
        'ucrSavePlot
        '
        Me.ucrSavePlot.Location = New System.Drawing.Point(10, 385)
        Me.ucrSavePlot.Name = "ucrSavePlot"
        Me.ucrSavePlot.Size = New System.Drawing.Size(265, 20)
        Me.ucrSavePlot.TabIndex = 13
        '
        'dlgTimeSeriesPlot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(422, 482)
        Me.Controls.Add(Me.ucrSavePlot)
        Me.Controls.Add(Me.cmdSummaries)
        Me.Controls.Add(Me.ucrChkIncludePoints)
        Me.Controls.Add(Me.ucrChkIncludeMeanLines)
        Me.Controls.Add(Me.ucrChkNAValues)
        Me.Controls.Add(Me.ucrReceiverStation)
        Me.Controls.Add(Me.lblStation)
        Me.Controls.Add(Me.ucrReceiverTime)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.lblEstimates)
        Me.Controls.Add(Me.ucrReceiverEstimates)
        Me.Controls.Add(Me.ucrReceiverReference)
        Me.Controls.Add(Me.lblReferenceValues)
        Me.Controls.Add(Me.ucrSelectorTimeSeriesPlots)
        Me.Controls.Add(Me.ucrBase)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgTimeSeriesPlot"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Time Series Comparisons"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ucrBase As ucrButtons
    Friend WithEvents ucrSelectorTimeSeriesPlots As ucrSelectorByDataFrameAddRemove
    Friend WithEvents lblReferenceValues As Label
    Friend WithEvents ucrReceiverReference As ucrReceiverSingle
    Friend WithEvents ucrSavePlot As ucrSaveGraph
    Friend WithEvents cmdSummaries As Button
    Friend WithEvents ucrChkIncludePoints As ucrCheck
    Friend WithEvents ucrChkIncludeMeanLines As ucrCheck
    Friend WithEvents ucrChkNAValues As ucrCheck
    Friend WithEvents ucrReceiverStation As ucrReceiverSingle
    Friend WithEvents lblStation As Label
    Friend WithEvents ucrReceiverTime As ucrReceiverSingle
    Friend WithEvents lblTime As Label
    Friend WithEvents lblEstimates As Label
    Friend WithEvents ucrReceiverEstimates As ucrReceiverSingle
End Class
