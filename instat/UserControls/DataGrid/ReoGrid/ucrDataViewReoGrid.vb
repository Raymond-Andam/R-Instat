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

Imports unvell.ReoGrid
Imports unvell.ReoGrid.Events

Public Class ucrDataViewReoGrid
    Implements IDataViewGrid

    Public Event CellDataChanged() Implements IDataViewGrid.CellDataChanged

    Public Event PasteValuesToDataframe() Implements IDataViewGrid.PasteValuesToDataframe

    Public Event ReplaceValueInData(strNewValue As String, strColumnName As String, strRowText As String) Implements IDataViewGrid.ReplaceValueInData

    Public Event DeleteValueToDataframe() Implements IDataViewGrid.DeleteValuesToDataframe

    Public Event EditCell() Implements IDataViewGrid.EditCell

    Public Event FindRow() Implements IDataViewGrid.FindRow

    Public Event WorksheetChanged() Implements IDataViewGrid.WorksheetChanged

    Public Event WorksheetRemoved(worksheet As clsWorksheetAdapter) Implements IDataViewGrid.WorksheetRemoved

    Public Sub AddColumns(visiblePage As clsDataFramePage) Implements IDataViewGrid.AddColumns
        Dim workSheetColumnHeader As ColumnHeader
        Dim variableTextColour As Color

        grdData.CurrentWorksheet.Columns = visiblePage.lstColumns.Count

        If GetCurrentDataFrameFocus.clsFilterOrColumnSelection.bColumnSelectionApplied Then
            variableTextColour = Color.Red
        Else
            variableTextColour = Color.DarkBlue
        End If
        For i = 0 To visiblePage.lstColumns.Count - 1
            workSheetColumnHeader = grdData.CurrentWorksheet.ColumnHeaders(i)
            workSheetColumnHeader.Text = visiblePage.lstColumns(i).strDisplayName
            workSheetColumnHeader.TextColor = variableTextColour
            workSheetColumnHeader.Style.BackColor = visiblePage.lstColumns(i).clsBackGroundColour
        Next
    End Sub

    Public Sub AddRowData(dataFrame As clsDataFrame) Implements IDataViewGrid.AddRowData
        Dim textColour As Color
        Dim strRowNames As String()
        Dim strLongestRowHeaderText As String = ""
        If dataFrame.iDisplayedRowCount = 0 Then
            AddBlankRow(grdData.CurrentWorksheet)
            Exit Sub
        End If
        grdData.CurrentWorksheet.Rows = dataFrame.iDisplayedRowCount
        UpdateWorksheetSettings(grdData.CurrentWorksheet)

        If dataFrame.clsFilterOrColumnSelection.bFilterApplied Then
            textColour = Color.Red
        Else
            textColour = Color.DarkBlue
        End If

        strRowNames = dataFrame.DisplayedRowNames()
        For i = 0 To grdData.CurrentWorksheet.Rows - 1
            For j = 0 To grdData.CurrentWorksheet.Columns - 1
                Dim strData As String = dataFrame.DisplayedData(i, j)
                If grdData.CurrentWorksheet.ColumnHeaders.Item(j).Text.Contains("(LT)") Then
                    strData = GetInnerBracketedString(strData)
                End If
                grdData.CurrentWorksheet(row:=i, col:=j) = strData
            Next
            grdData.CurrentWorksheet.RowHeaders.Item(i).Text = strRowNames(i)
            grdData.CurrentWorksheet.RowHeaders(i).TextColor = textColour

            'get longest row header text
            If strRowNames(i).Length > strLongestRowHeaderText.Length Then
                strLongestRowHeaderText = strRowNames(i)
            End If
        Next

        If dataFrame.clsFilterOrColumnSelection.bFilterApplied Then
            grdData.CurrentWorksheet.ScrollToCell("A1") ' will always set the scrollbar at the top.
        End If

        'todo. As of 30/05/2022, the reogrid control version used did not have this setting option
        'see issue #7221 for more information.
        'get pixel size equivalent of the longest row header text
        'and use it as the row header width.
        'TODO. Note , the text length may not always reflect the correct pixel to use. See comments in issue #7221 
        grdData.CurrentWorksheet.RowHeaderWidth = TextRenderer.MeasureText(strLongestRowHeaderText, Me.Font).Width
    End Sub

    Private Sub RefreshSingleCell(iColumn As Integer, iRow As Integer)
        grdData.CurrentWorksheet(iRow, iColumn) = GetCurrentDataFrameFocus.DisplayedData(iRow, iColumn)
    End Sub

    Private Function GetInnerBracketedString(strData As String) As String
        Dim intFirstRightBracket As Integer = InStr(strData, ")")
        Dim intLastLeftBracket As Integer = InStrRev(strData, "(")
        If intFirstRightBracket = 0 Or intLastLeftBracket = 0 Then
            Return strData
        Else
            Dim strOutput As String = Mid(strData, intLastLeftBracket + 1, intFirstRightBracket - intLastLeftBracket - 1)
            Return strOutput
        End If
    End Function

    Public Function GetSelectedColumns() As List(Of clsColumnHeaderDisplay) Implements IDataViewGrid.GetSelectedColumns
        Dim lstColumns As New List(Of clsColumnHeaderDisplay)
        For i As Integer = grdData.CurrentWorksheet.SelectionRange.Col To grdData.CurrentWorksheet.SelectionRange.Col + grdData.CurrentWorksheet.SelectionRange.Cols - 1
            lstColumns.Add(GetCurrentDataFrameFocus().clsVisibleDataFramePage.lstColumns(i))
        Next
        Return lstColumns
    End Function

    Public Function GetWorksheetCount() As Integer Implements IDataViewGrid.GetWorksheetCount
        Return grdData.Worksheets.Count
    End Function

    Protected Overrides Sub AttachEventsToWorksheet(worksheet As Worksheet)
        AddHandler worksheet.AfterCellEdit, AddressOf Worksheet_AfterCellEdit
        AddHandler worksheet.BeforeCut, AddressOf Worksheet_BeforeCut
        AddHandler worksheet.BeforePaste, AddressOf Worksheet_BeforePaste
        AddHandler worksheet.BeforeRangeMove, AddressOf Worksheet_BeforeRangeMove
        AddHandler worksheet.BeforeCellKeyDown, AddressOf Worksheet_BeforeCellsKeyDown
        AddHandler worksheet.CellDataChanged, AddressOf Worksheet_CellDataChanged
    End Sub

    Private Sub AddBlankRow(workSheet As Worksheet)
        workSheet.Rows = 1
        For i = 0 To workSheet.Columns - 1
            workSheet(0, col:=i) = ""
        Next
        workSheet.SetRowsHeight(0, 1, 0.1)
        workSheet.SetSettings(unvell.ReoGrid.WorksheetSettings.Edit_AllowAdjustRowHeight, False)
        Exit Sub
    End Sub

    Private Function GetCurrentDataFrameFocus() As clsDataFrame
        Return _clsDataBook.GetDataFrame(grdData.CurrentWorksheet.Name)
    End Function

    Private Sub grdData_CurrentWorksheetChanged(sender As Object, e As EventArgs) Handles grdData.CurrentWorksheetChanged, grdData.WorksheetInserted
        RaiseEvent WorksheetChanged()
    End Sub

    Private Sub grdData_WorksheetRemoved(sender As Object, e As WorksheetRemovedEventArgs) Handles grdData.WorksheetRemoved
        RaiseEvent WorksheetRemoved(New clsWorksheetAdapter(e.Worksheet))
    End Sub

    Private Sub ucrDataViewReoGrid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Disable Autoformat cell
        'This needs to be added at the part when we are writing data to the grid, not here
        'Needs discussion, with this the grid can show NA's
        grdData.SetSettings(unvell.ReoGrid.WorksheetSettings.Edit_AutoFormatCell, False)
        grdData.SheetTabWidth = 450
    End Sub

    Private Sub UpdateWorksheetSettings(workSheet As Worksheet)
        workSheet.SetSettings(unvell.ReoGrid.WorksheetSettings.Edit_AllowAdjustRowHeight, True)
        workSheet.SetRowsHeight(0, 1, 20)
        Dim rngDataRange As New RangePosition(0, 0, workSheet.Rows, workSheet.Columns)
        workSheet.SetRangeDataFormat(rngDataRange, DataFormat.CellDataFormatFlag.Text)
    End Sub

    Private Sub Worksheet_AfterCellEdit(sender As Object, e As CellAfterEditEventArgs)
        RaiseEvent ReplaceValueInData(e.NewData.ToString(),
                           GetCurrentDataFrameFocus().clsVisibleDataFramePage.lstColumns(e.Cell.Column).strName,
                           GetCurrentDataFrameFocus().clsVisibleDataFramePage.RowNames()(e.Cell.Row))
        e.EndReason = unvell.ReoGrid.EndEditReason.Cancel
        RefreshSingleCell(e.Cell.Column, e.Cell.Row)
    End Sub


    Private Sub Worksheet_BeforePaste(sender As Object, e As BeforeRangeOperationEventArgs)
        e.IsCancelled = True 'prevents pasted data from being added directly into the data view
        'validate columns
        If e.Range.EndCol >= GetCurrentDataFrameFocus().clsVisibleDataFramePage.lstColumns.Count Then
            'this happens when Ctrl + V is pressed and the data to be pasted has more columns
            'than columns between start and end column
            MsgBox("Columns copied are more than the current data frame columns.", MsgBoxStyle.Critical, "Excess Columns")
            Exit Sub
        End If
        'TODO check see if pasted range is same as selected
        RaiseEvent PasteValuesToDataframe()
    End Sub

    Private Sub Worksheet_CellDataChanged(sender As Object, e As CellEventArgs)
        RaiseEvent CellDataChanged()
    End Sub

    Private Sub Worksheet_BeforeCellsKeyDown(sender As Object, e As BeforeCellKeyDownEventArgs)
        If e.KeyCode = unvell.ReoGrid.Interaction.KeyCode.Delete OrElse e.KeyCode = unvell.ReoGrid.Interaction.KeyCode.Back Then
            e.IsCancelled = True
            RaiseEvent DeleteValueToDataframe()
        End If
    End Sub

    Private Sub Worksheet_AfterCellKeyDownEventArgs(sender As Object, e As KeyEventArgs) Handles grdData.KeyDown

        If (e.KeyCode And Not Keys.Modifiers) = Keys.E AndAlso e.Modifiers = Keys.Control Then
            'e.IsCancelled = True
            RaiseEvent EditCell()
        ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.F AndAlso e.Modifiers = Keys.Control Then
            RaiseEvent FindRow()
        End If
    End Sub

    Private Function GetColumnIndex(currWorkSheet As Worksheet, strColName As String) As Integer
        If currWorkSheet IsNot Nothing Then
            For i As Integer = 0 To currWorkSheet.Columns - 1
                Dim strCol As String = currWorkSheet.ColumnHeaders(i).Text
                If Trim(strCol.Split("(")(0)) = strColName.Replace("""", "") Then
                    Return i
                End If
            Next
        End If
        Return -1
    End Function

    Private Function GetRowIndex(currWorkSheet As Worksheet, strRowName As String) As Integer
        If currWorkSheet IsNot Nothing Then
            For i As Integer = 0 To currWorkSheet.Rows - 1
                Dim strCol As String = currWorkSheet.RowHeaders(i).Text
                If strCol = strRowName Then
                    Return i
                End If
            Next
        End If
        Return -1
    End Function

    Private Function GetRowsIndexes(currWorkSheet As Worksheet, lstRows As List(Of String)) As List(Of String)
        Dim lstRowsIndexes As New List(Of String)
        If currWorkSheet IsNot Nothing Then
            For i As Integer = 0 To lstRows.Count - 1
                For j As Integer = 0 To currWorkSheet.Rows - 1
                    Dim strCol As String = currWorkSheet.RowHeaders(j).Text
                    If strCol = lstRows(i) Then
                        lstRowsIndexes.Add(j)
                    End If
                Next
            Next
        End If
        Return lstRowsIndexes
    End Function

    Private Sub ScrollToCellPos(currWorkSheet As Worksheet, iRow As Integer, iCol As Integer)
        currWorkSheet.FocusPos = currWorkSheet.Cells(row:=iRow, col:=iCol).Position
        currWorkSheet.ScrollToCell(currWorkSheet.Cells(row:=iRow, col:=iCol).Address)
    End Sub

    Private Sub SetRowOrCellBackgroundColor(currWorkSheet As Worksheet, rowNumbers As List(Of String), colIndex As Integer, bCellOrRow As Boolean, color As Color)
        ' Create a new style object for the row background color
        Dim rowStyle As WorksheetRangeStyle = New WorksheetRangeStyle With {
            .Flag = PlainStyleFlag.BackColor,
            .BackColor = color
        }

        ' Iterate over the row numbers and apply the style to each row
        For Each rowNumber As Integer In rowNumbers
            ' Check if the row index is within the valid range
            If rowNumber >= 0 AndAlso rowNumber < currWorkSheet.RowCount Then
                If bCellOrRow Then
                    ' Apply the row style to the entire row
                    currWorkSheet.Cells(rowNumber, colIndex).Style.BackColor = color
                Else
                    currWorkSheet.SetRangeStyles(New RangePosition(rowNumber, 0, 1, colIndex), rowStyle)
                End If
            End If
        Next
    End Sub

    Public Sub SearchInGrid(rowNumbers As List(Of String), strColumn As String, Optional iRow As Integer = 0,
                            Optional bCellOrRow As Boolean = False) Implements IDataViewGrid.SearchInGrid
        Dim currSheet = grdData.CurrentWorksheet

        If currSheet.RowHeaders.Any(Function(x) x.Text = iRow) Then
            Dim iRowIndex = GetRowIndex(currSheet, iRow)
            Dim iColIndex As Integer = GetColumnIndex(currSheet, strColumn)
            If bCellOrRow Then
                ScrollToCellPos(currWorkSheet:=currSheet, iRow:=iRowIndex, iCol:=iColIndex)
                SetRowOrCellBackgroundColor(currWorkSheet:=currSheet, rowNumbers:=GetRowsIndexes(currSheet, rowNumbers),
                                     colIndex:=iColIndex, bCellOrRow:=bCellOrRow, color:=Color.LightGreen)
            Else
                ScrollToCellPos(currWorkSheet:=currSheet, iRow:=iRowIndex, iCol:=iColIndex)
                SetRowOrCellBackgroundColor(currWorkSheet:=currSheet, rowNumbers:=GetRowsIndexes(currSheet, rowNumbers),
                                     colIndex:=currSheet.ColumnCount, bCellOrRow:=bCellOrRow, color:=Color.LightGreen)
            End If
        End If
    End Sub


End Class
