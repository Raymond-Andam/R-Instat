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

Imports RDotNet
Imports instat.Translations
Public Class dlgScript
    Private strComment As String = "Code generated by the dialog, Script"
    Private bFirstload As Boolean = True
    Private clsSaveDataFunction As New RFunction
    Private dctOutputObjectTypes As New Dictionary(Of String, String)
    Private dctOutputObjectFormats As New Dictionary(Of String, String)

    Private Sub dlgScript_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If bFirstload Then
            InitialiseDialog()
            SetDefaults()
            bFirstload = False
        End If
        autoTranslate(Me)
    End Sub

    Private Sub InitialiseDialog()

        ' Supported output object types and formats
        dctOutputObjectTypes.Add("Summary", RObjectTypeLabel.Summary)
        dctOutputObjectTypes.Add("Table", RObjectTypeLabel.Table)
        dctOutputObjectTypes.Add("Graph", RObjectTypeLabel.Graph)
        dctOutputObjectTypes.Add("Model", RObjectTypeLabel.Model)
        dctOutputObjectTypes.Add("Structure", RObjectTypeLabel.StructureLabel)

        dctOutputObjectFormats.Add("Image", RObjectFormat.Image)
        dctOutputObjectFormats.Add("Text", RObjectFormat.Text)
        dctOutputObjectFormats.Add("Html", RObjectFormat.Html)

        '--------------------------------
        'save controls
        ucrPnlSaveData.AddRadioButton(rdoSaveDataFrame)
        ucrPnlSaveData.AddRadioButton(rdoSaveColumn)
        ucrPnlSaveData.AddRadioButton(rdoSaveOutputObject)

        ucrCboSaveOutputObjectType.SetItems(dctOutputObjectTypes, bSetConditions:=False)
        ucrCboSaveOutputObjectType.SetDropDownStyleAsNonEditable()
        ucrCboSaveOutputObjectType.SetLinkedDisplayControl(lblSaveObjectType)
        ucrCboSaveOutputObjectType.GetSetSelectedIndex = 0

        ucrCboSaveOutputObjectFormat.SetItems(dctOutputObjectFormats, bSetConditions:=False)
        ucrCboSaveOutputObjectFormat.SetDropDownStyleAsNonEditable()
        ucrCboSaveOutputObjectFormat.SetLinkedDisplayControl(lblSaveObjectFormat)
        ucrCboSaveOutputObjectFormat.GetSetSelectedIndex = 0


        ucrSaveData.SetLabelText("Save Graph")
        ucrSaveData.SetIsComboBox()
        ucrSaveData.SetDataFrameSelector(ucrDataFrameSaveOutputSelect)


        '--------------------------------
        'Get data controls

        ucrPnlGetData.AddRadioButton(rdoGetDataFrame)
        ucrPnlGetData.AddRadioButton(rdoGetColumn)
        ucrPnlGetData.AddRadioButton(rdoGetOutputObject)

        ucrDataFrameGetDF.SetLabelText("Get Data Frame:")

        ucrReceiverGetColumns.Selector = ucrSelectorGetObject
        ucrReceiverGetColumns.SetLinkedDisplayControl(lblGetColumn)

        ucrCboGetOutputObjectType.SetItems(dctOutputObjectTypes, bSetConditions:=False)
        ucrCboGetOutputObjectType.SetDropDownStyleAsNonEditable()
        ucrCboGetOutputObjectType.SetLinkedDisplayControl(lblGetObjectType)
        ucrCboGetOutputObjectType.GetSetSelectedIndex = 0

        ucrReceiverGetOutputObject.Selector = ucrSelectorGetObject
        ucrReceiverGetOutputObject.SetLinkedDisplayControl(lblGetOutputObject)

        '--------------------------------
        ' Command controls

        'todo. this combo box can be a custom package control in future. Its also needed in dlgHelpVignettes
        ucrCboCommandPackage.SetParameter(New RParameter("package", 0))
        ucrCboCommandPackage.SetItems(GetPackages(), bAddConditions:=True)
        ucrCboCommandPackage.SetDropDownStyleAsNonEditable()


        ucrPnlCommands.AddRadioButton(rdoCommandPackage)
        ucrPnlCommands.AddRadioButton(rdoCommandObject)

        '--------------------------------
        'Get example controls
        ucrPnlExample.AddRadioButton(rdoExampleData)
        ucrPnlExample.AddRadioButton(rdoExampleFunction)

        ucrCboExamplePackages.SetItems(GetPackages(), bAddConditions:=False)
        ucrCboExamplePackages.SetDropDownStyleAsNonEditable()

        '-------------------------------
        ' base buttons controls not supported in this dialog
        ucrBase.bAppendScriptsAtCurrentScriptWindowCursorPosition = True

        ' hide controls not supported in this dialog
        ucrBase.chkComment.Checked = False
        ucrBase.chkComment.Visible = False
        ucrBase.txtComment.Visible = False
        ucrBase.bAddScriptToScriptWindowOnClickOk = False

    End Sub

    'todo. this function should eventually be removed once we have a control that displays packages
    Private Function GetPackages() As String()
        Dim arrAvailablePackages() As String = {}
        Dim clsGetPackages As New RFunction
        clsGetPackages.SetRCommand("get_installed_packages_with_data")
        clsGetPackages.AddParameter("with_data", "FALSE")
        Dim expPackageNames As SymbolicExpression = frmMain.clsRLink.RunInternalScriptGetValue(clsGetPackages.ToScript(), bSeparateThread:=False, bSilent:=True)
        If expPackageNames IsNot Nothing AndAlso expPackageNames.Type <> Internals.SymbolicExpressionType.Null Then
            arrAvailablePackages = expPackageNames.AsCharacter.ToArray
            Array.Sort(arrAvailablePackages)
        End If
        Return arrAvailablePackages
    End Function

    Private Sub SetDefaults()

        ' Examples controls
        rdoExampleData.Checked = True

        ' Command controls
        rdoCommandPackage.Checked = True
        ucrCboCommandPackage.GetSetSelectedIndex = -1
        ucrInputRemoveObjects.Reset()

        ' Save controls reset
        ucrSaveData.SetRCode(clsSaveDataFunction, True)
        ucrSaveData.Reset()
        rdoSaveDataFrame.Checked = True
        ucrDataFrameSaveOutputSelect.Reset()

        ' Get controls reset
        ucrSelectorGetObject.Reset()
        ucrCboGetOutputObjectType.GetSetSelectedIndex = 0
        ucrDataFrameGetDF.Reset()
        rdoGetDataFrame.Checked = True


        'activate the selected tab to library tab
        tbFeatures.SelectedIndex = -1
        tbFeatures.SelectedTab = tbPageSaveData

    End Sub

    Private Sub ucrPnlSaveData_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrPnlSaveData.ControlValueChanged
        ucrDataFrameSaveOutputSelect.SetVisible(False)
        ucrCboSaveOutputObjectType.SetVisible(False)
        ucrCboSaveOutputObjectFormat.SetVisible(False)
        If rdoSaveDataFrame.Checked Then
            ucrSaveData.Location = New Point(ucrSaveData.Location.X, ucrDataFrameSaveOutputSelect.Location.Y)
            SetupSaveDataControl("Data Frame", RObjectTypeLabel.Dataframe, "")
        ElseIf rdoSaveColumn.Checked Then
            ucrSaveData.Location = New Point(ucrSaveData.Location.X, ucrCboSaveOutputObjectType.Location.Y)
            ucrDataFrameSaveOutputSelect.SetVisible(True)
            SetupSaveDataControl("Column", RObjectTypeLabel.Column, "")
        ElseIf rdoSaveOutputObject.Checked Then
            ucrSaveData.Location = New Point(ucrSaveData.Location.X, ucrCboSaveOutputObjectFormat.Location.Y + 33)
            ucrDataFrameSaveOutputSelect.SetVisible(True)
            ucrCboSaveOutputObjectType.SetVisible(True)
            ucrCboSaveOutputObjectFormat.SetVisible(True)
            SetupSaveDataControl(ucrCboSaveOutputObjectType.GetText(), dctOutputObjectTypes.Item(ucrCboSaveOutputObjectType.GetText()), dctOutputObjectFormats.Item(ucrCboSaveOutputObjectFormat.GetText()))
        End If
    End Sub

    Private Sub ucrCboSaveOutputObjectType_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrCboSaveOutputObjectType.ControlValueChanged, ucrCboSaveOutputObjectFormat.ControlValueChanged
        If Not ucrCboSaveOutputObjectType.IsEmpty() AndAlso Not ucrCboSaveOutputObjectFormat.IsEmpty() Then
            SetupSaveDataControl(ucrCboSaveOutputObjectType.GetText(), dctOutputObjectTypes.Item(ucrCboSaveOutputObjectType.GetText()), dctOutputObjectFormats.Item(ucrCboSaveOutputObjectFormat.GetText()))
        End If
    End Sub

    Private Sub SetupSaveDataControl(strLabel As String, strDataType As String, strFormat As String)

        ucrSaveData.SetSaveType(strDataType, strFormat)
        ucrSaveData.SetLabelText(strLabel)
        ucrSaveData.SetName("")

        If strDataType = RObjectTypeLabel.Dataframe Then
            ucrSaveData.SetIsTextBox()
        Else
            ucrSaveData.SetIsComboBox()
        End If
    End Sub

    Private Sub ucrSaveData_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrSaveData.ControlContentsChanged
        Dim strScript As String = ""

        If ucrSaveData.IsComplete Then
            ' R code is not automatiucally updated by save control when control contents changed event is raised by the control
            ucrSaveData.UpdateRCode()

            Dim strDataType As String = ""
            If rdoSaveDataFrame.Checked Then
                strDataType = "data frame"
            ElseIf rdoSaveColumn.Checked Then
                strDataType = "column"
            ElseIf rdoSaveOutputObject.Checked Then
                strDataType = ucrCboSaveOutputObjectType.GetText().ToLower()
            End If

            strScript = "# Save " & strDataType & " """ & ucrSaveData.GetText() & """" & Environment.NewLine & clsSaveDataFunction.Clone.ToScript()
        End If

        PreviewScript(strScript)
    End Sub

    Private Sub ucrPnlGetData_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrPnlGetData.ControlValueChanged
        ucrDataFrameGetDF.SetVisible(False)
        ucrCboGetOutputObjectType.SetVisible(False)
        ucrSelectorGetObject.SetVisible(False)
        ucrReceiverGetColumns.SetVisible(False)
        ucrReceiverGetOutputObject.SetVisible(False)
        PreviewScript("")
        If rdoGetDataFrame.Checked Then
            ucrDataFrameGetDF.SetVisible(True)
            ucrReceiverGetOutputObject.SetVisible(False)
            ucrDataFrameGetDF.Reset()
        ElseIf rdoGetColumn.Checked Then
            ucrSelectorGetObject.SetVisible(True)
            ucrReceiverGetColumns.SetVisible(True)
            ucrReceiverGetColumns.SetMeAsReceiver()
            ucrReceiverGetColumns.Clear()
        ElseIf rdoGetOutputObject.Checked Then
            ucrSelectorGetObject.SetVisible(True)
            ucrCboGetOutputObjectType.SetVisible(True)
            ucrReceiverGetOutputObject.SetVisible(True)
            SetupReceiverForGetOutputObject(ucrCboGetOutputObjectType.GetText(), dctOutputObjectTypes.Item(ucrCboGetOutputObjectType.GetText()))
            ucrReceiverGetOutputObject.SetMeAsReceiver()
        End If
    End Sub

    Private Sub SetupReceiverForGetOutputObject(strLabel As String, strDataType As String)
        ucrReceiverGetOutputObject.Clear()
        lblGetOutputObject.Text = strLabel & ":"
        ucrReceiverGetOutputObject.SetSelectorHeading(strLabel)
        ucrReceiverGetOutputObject.SetItemType(strDataType)
    End Sub

    Private Sub ucrDataFrameGet_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrDataFrameGetDF.ControlContentsChanged
        Dim strScript As String = ""

        If String.IsNullOrEmpty(ucrDataFrameGetDF.strCurrDataFrame) Then
            Dim strAssignedScript As String = ""
            ucrDataFrameGetDF.clsCurrDataFrame.Clone().ToScript(strAssignedScript)
            strScript = "# Get data frame """ & ucrDataFrameGetDF.strCurrDataFrame & """" & Environment.NewLine & strAssignedScript
        End If

        PreviewScript(strScript)
    End Sub

    Private Sub ucrReceiverGetColumns_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrReceiverGetColumns.ControlContentsChanged
        Dim strScript As String = ""

        If Not ucrReceiverGetColumns.IsEmpty() Then
            Dim clsRFunction As RFunction = ucrReceiverGetColumns.GetVariables()
            Dim strAssignedScript As String = ""
            clsRFunction.SetAssignTo(ucrSelectorGetObject.strCurrentDataFrame & "_cols")
            clsRFunction.ToScript(strScript:=strAssignedScript)
            strScript = "# Get column(s) " & String.Join(",", ucrReceiverGetColumns.GetVariableNamesList(bWithQuotes:=True)) & Environment.NewLine & strAssignedScript
        End If

        PreviewScript(strScript)
    End Sub

    Private Sub ucrInputGetObjectType_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrCboGetOutputObjectType.ControlValueChanged
        If Not ucrCboGetOutputObjectType.IsEmpty() Then
            SetupReceiverForGetOutputObject(ucrCboGetOutputObjectType.GetText(), dctOutputObjectTypes.Item(ucrCboGetOutputObjectType.GetText()))
        End If
    End Sub

    Private Sub ucrReceiverGetOutputObject_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrReceiverGetOutputObject.ControlContentsChanged
        Dim strScript As String = ""

        If Not ucrReceiverGetOutputObject.IsEmpty() Then
            Dim clsRFunction As RFunction = ucrReceiverGetOutputObject.GetVariables()
            Dim strAssignedScript As String = ""
            clsRFunction.ToScript(strScript:=strAssignedScript)
            strScript = "# Get " & ucrCboGetOutputObjectType.GetText().ToLower() & " " & ucrReceiverGetOutputObject.GetVariableNames(bWithQuotes:=True) & Environment.NewLine & strAssignedScript
        End If

        PreviewScript(strScript)
    End Sub

    Private Sub ucrPnlCommands_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrPnlCommands.ControlValueChanged
        ucrCboCommandPackage.SetVisible(False)
        ucrInputRemoveObjects.SetVisible(False)
        If rdoCommandPackage.Checked Then
            ucrCboCommandPackage.SetVisible(True)
            ucrCboCommandPackage.OnControlValueChanged()
        ElseIf rdoCommandObject.Checked Then
            ucrInputRemoveObjects.SetVisible(True)
            ucrInputRemoveObjects.OnControlValueChanged()
        End If
    End Sub

    Private Sub ucrCboLibPackage_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrCboCommandPackage.ControlValueChanged
        Dim strScript As String = ""

        If Not ucrCboCommandPackage.IsEmpty() Then
            Dim clsLibraryFunction As New RFunction
            clsLibraryFunction.SetRCommand("library")
            clsLibraryFunction.AddParameter("package", Chr(34) & ucrCboCommandPackage.GetText() & Chr(34))
            strScript = "# Load library """ & ucrCboCommandPackage.GetText() & """" & Environment.NewLine & clsLibraryFunction.ToScript
        End If

        PreviewScript(strScript)
    End Sub

    Private Sub ucrInputRemoveObject_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrInputRemoveObjects.ControlContentsChanged
        Dim strScript As String = ""

        If Not ucrInputRemoveObjects.IsEmpty() Then
            ' Create function to remove the objects added in the script
            Dim lstAssignToStrings As String() = ucrInputRemoveObjects.GetText().Split(",")
            Dim clsRemoveFunc As New RFunction
            Dim clsRemoveListFun As New RFunction
            clsRemoveFunc.SetRCommand("rm")
            clsRemoveListFun.SetRCommand("c")

            For i As Integer = 0 To lstAssignToStrings.Count - 1
                lstAssignToStrings.SetValue(Chr(34) & lstAssignToStrings(i) & Chr(34), i)
                clsRemoveListFun.AddParameter(i, lstAssignToStrings(i), bIncludeArgumentName:=False)
            Next
            clsRemoveFunc.AddParameter("list", clsRFunctionParameter:=clsRemoveListFun)
            strScript = "# Remove object(s) " & String.Join(",", lstAssignToStrings) & Environment.NewLine & clsRemoveFunc.ToScript()
        End If

        PreviewScript(strScript)
    End Sub

    Private Sub ucrComboGetPackages_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrCboExamplePackages.ControlValueChanged, ucrPnlExample.ControlValueChanged
        PreviewScript("")
        lstExampleCollection.Items.Clear()

        If ucrCboExamplePackages.IsEmpty Then
            Exit Sub
        End If

        Dim strSelectedPackage As String = ucrCboExamplePackages.GetText()

        If rdoExampleData.Checked Then
            lstExampleCollection.Columns(0).Text = "Data"
            lstExampleCollection.Items.AddRange(GetDatasets(strSelectedPackage))
        ElseIf rdoExampleFunction.Checked Then
            lstExampleCollection.Columns(0).Text = "Functions"
            lstExampleCollection.Items.AddRange(GetFunctions(strSelectedPackage))
        End If
        lstExampleCollection.Select()

    End Sub

    Private Function GetDatasets(strPackage As String) As ListViewItem()

        Dim dfDataframe As DataFrame = frmMain.clsRLink.RunInternalScriptGetValue(
            "data.frame(data(package =" & Chr(34) & strPackage & Chr(34) & ")$results)[ ,3:4]", bSilent:=True)?.AsDataFrame()

        If dfDataframe Is Nothing Then
            Return {}
        End If

        Dim lstViewItems(dfDataframe.RowCount - 1) As ListViewItem
        For i As Integer = 0 To dfDataframe.RowCount - 1
            Dim lstViewItem As New ListViewItem With {
                .Text = dfDataframe(i, 0)
            }

            'lstViewItem.SubItems.Add(dfDataframe(i, 0))
            lstViewItem.SubItems.Add(If(dfDataframe.ColumnCount > 1, dfDataframe(i, 1), ""))
            lstViewItems(i) = lstViewItem
        Next
        Return lstViewItems

    End Function

    Private Function GetFunctions(strPackage As String) As ListViewItem()

        Dim expTemp As GenericVector = frmMain.clsRLink.RunInternalScriptGetValue("ls(pos = asNamespace(" & Chr(34) & strPackage & Chr(34) & "))", bSilent:=True)?.AsList()

        If expTemp Is Nothing Then
            Return {}
        End If

        Dim lstViewItems(expTemp.Length - 1) As ListViewItem
        For i = 0 To expTemp.Length - 1
            Dim lstViewItem As New ListViewItem With {
                .Text = expTemp.AsCharacter(i)
            }
            lstViewItem.SubItems.Add("")
            lstViewItems(i) = lstViewItem
        Next
        Return lstViewItems

    End Function

    Private Sub lstExampleCollection_Click(sender As Object, e As EventArgs) Handles lstExampleCollection.Click
        If lstExampleCollection.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        Dim strTopic As String = lstExampleCollection.SelectedItems(0).SubItems(0).Text
        Try

            Dim clsLibraryExpFunction As New RFunction
            clsLibraryExpFunction.SetRCommand("getExample")
            clsLibraryExpFunction.AddParameter("package", Chr(34) & ucrCboExamplePackages.GetText() & Chr(34), iPosition:=1)
            clsLibraryExpFunction.AddParameter("topic", Chr(34) & strTopic & Chr(34), iPosition:=0)

            Dim strExample As String = frmMain.clsRLink.RunInternalScriptGetValue(clsLibraryExpFunction.Clone.ToScript(), bSilent:=True).AsCharacter(0)
            PreviewScript(strExample)
        Catch ex As Exception
            MsgBox(strTopic & " has a help file but no examples.")
        End Try
    End Sub

    Private Sub txtScript_TextChanged(sender As Object, e As EventArgs) Handles txtScript.TextChanged
        ucrBase.clsRsyntax.SetCommandString(txtScript.Text)
        ucrBase.OKEnabled(txtScript.Text.Length > 0)
    End Sub

    Private Sub ucrBase_ClickReset(sender As Object, e As EventArgs) Handles ucrBase.ClickReset
        SetDefaults()
    End Sub

    Private Sub tbFeatures_Selected(sender As Object, e As TabControlEventArgs) Handles tbFeatures.Selected
        If e.TabPage Is tbPageGetData Then
            rdoGetDataFrame.Checked = True
            ucrPnlGetData.OnControlValueChanged()
        ElseIf e.TabPage Is tbPageSaveData Then
            rdoSaveDataFrame.Checked = True
            ucrPnlSaveData.OnControlValueChanged()
        ElseIf e.TabPage Is tbPageCommand Then
            'alwys reset the common controls to be blank.
            'the controls functionalities are not related
            ucrCboCommandPackage.GetSetSelectedIndex = -1
            ucrInputRemoveObjects.SetName("")
            PreviewScript("")
        ElseIf e.TabPage Is tbPageExamples Then
            ucrCboExamplePackages.OnControlValueChanged()
        End If
    End Sub

    Private Sub PreviewScript(strNewScript As String)
        txtScript.Text = strNewScript & Environment.NewLine
        txtScript.SelectionStart = txtScript.Text.Length
        txtScript.ScrollToCaret()
        txtScript.Refresh()
    End Sub


End Class