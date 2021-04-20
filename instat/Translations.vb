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
Imports System.Reflection

Public Class Translations

    '''--------------------------------------------------------------------------------------------
    ''' <summary>
    '''     Attempts to translate all the text in <paramref name="clsControl"/> to the currently
    '''     configured language.
    ''' </summary>
    '''
    ''' <param name="clsControl">    The WinForm control to translate. </param>
    '''--------------------------------------------------------------------------------------------
    Public Shared Sub autoTranslate(clsControl As Control)
        If IsNothing(TryCast(clsControl, Form)) Then
            Exit Sub
        End If

        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateForm(clsControl, GetDbPath(), GetLanguageCode()))
    End Sub

    '''--------------------------------------------------------------------------------------------
    ''' <summary>
    '''     Attempts to translate all the text in the menu items in <paramref name="tsCollection"/> 
    '''     to  the currently configured language.
    ''' </summary>
    '''
    ''' <param name="tsCollection">     The WinForm menu items to translate. </param>
    ''' <param name="ctrParent">        The WinForm control that is the parent of the menu. </param>
    '''--------------------------------------------------------------------------------------------
    Public Shared Sub translateMenu(tsCollection As ToolStripItemCollection, ctrParent As Control)
        ' The 'WriteCsvFile' function call below should normally be commented out. 
        ' It only needs be uncommented and executed once, prior to each new release.
        'WriteCsvFile()

        If IsNothing(tsCollection) OrElse IsNothing(ctrParent) OrElse IsNothing(TryCast(ctrParent, Form)) Then
            Exit Sub
        End If

        'translate the main form, including all its output windows and menus
        Dim strDbPath = GetDbPath()
        Dim strLanguageCode = GetLanguageCode()
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateForm(ctrParent, strDbPath, strLanguageCode))

        'The right mouse button menus for the 6 output windows are not accessible via 
        '    the control lists. Therefore, translate these menus explicitly
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrOutput.Name, frmMain.ucrOutput.mnuContextRTB.Items, strDbPath, strLanguageCode))
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrColumnMeta.Name, frmMain.ucrColumnMeta.cellContextMenuStrip.Items, strDbPath, strLanguageCode))
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrColumnMeta.Name, frmMain.ucrColumnMeta.grdVariables.RowHeaderContextMenuStrip.Items, strDbPath, strLanguageCode))
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrColumnMeta.Name, frmMain.ucrColumnMeta.statusColumnMenu.Items, strDbPath, strLanguageCode))
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrDataFrameMeta.Name, frmMain.ucrDataFrameMeta.cellContextMenuStrip.Items, strDbPath, strLanguageCode))
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrDataFrameMeta.Name, frmMain.ucrDataFrameMeta.rowRightClickMenu.Items, strDbPath, strLanguageCode))
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrLogWindow.Name, frmMain.ucrLogWindow.mnuContextLogFile.Items, strDbPath, strLanguageCode))
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrScriptWindow.Name, frmMain.ucrScriptWindow.mnuContextScript.Items, strDbPath, strLanguageCode))
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrDataViewer.Name, frmMain.ucrDataViewer.grdData.RowHeaderContextMenuStrip.Items, strDbPath, strLanguageCode))
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrDataViewer.Name, frmMain.ucrDataViewer.grdData.ColumnHeaderContextMenuStrip.Items, strDbPath, strLanguageCode))
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrDataViewer.Name, frmMain.ucrDataViewer.grdData.ContextMenuStrip.Items, strDbPath, strLanguageCode))
        HandleError(TranslateWinForm.clsTranslateWinForm.TranslateMenuItems(frmMain.ucrDataViewer.Name, frmMain.ucrDataViewer.grdData.SheetTabContextMenuStrip.Items, strDbPath, strLanguageCode))
    End Sub

    '''--------------------------------------------------------------------------------------------
    ''' <summary>   TODO Gets a translation. </summary>
    '''
    ''' <param name="strText">  The text. </param>
    '''
    ''' <returns>   The translation. </returns>
    '''--------------------------------------------------------------------------------------------
    Public Shared Function GetTranslation(strText As String) As String
        If String.IsNullOrEmpty(strText) Then
            Return ""
        End If

        Return TranslateWinForm.clsTranslateWinForm.GetTranslation(strText, GetDbPath(), GetLanguageCode())
    End Function


    '''--------------------------------------------------------------------------------------------
    ''' <summary>   
    '''    Displays an error message box that displays <paramref name="strErrorMsg"/>.
    '''    If <paramref name="strErrorMsg"/> is empty then does nothing.
    ''' </summary>
    '''
    ''' <param name="strErrorMsg">  The error message to display. </param>
    '''--------------------------------------------------------------------------------------------
    Private Shared Sub HandleError(strErrorMsg As String)
        If Not String.IsNullOrEmpty(strErrorMsg) Then
            MsgBox(strErrorMsg, MsgBoxStyle.Exclamation)
        End If
    End Sub

    '''--------------------------------------------------------------------------------------------
    ''' <summary>   Returns the language code for the currently configured language (e.g. 'en' for 
    '''             English, 'fr' for French etc.). </summary>
    '''
    ''' <returns>   The language code for the currently configured language (e.g. 'en' for
    '''             English, 'fr' for French etc.). </returns>
    '''--------------------------------------------------------------------------------------------
    Private Shared Function GetLanguageCode() As String
        If IsNothing(frmMain) OrElse IsNothing(frmMain.clsInstatOptions) OrElse
                IsNothing(frmMain.clsInstatOptions.strLanguageCultureCode) Then
            Return "en"
        End If
        Dim arrLanguageCodes As String() = frmMain.clsInstatOptions.strLanguageCultureCode.Split(New Char() {"-"c})
        Return arrLanguageCodes(0)
    End Function

    '''--------------------------------------------------------------------------------------------
    ''' <summary>   Returns the full path of the SQLite translations database file. </summary>
    '''
    ''' <returns>   The full path of the SQLite translations database file. </returns>
    '''--------------------------------------------------------------------------------------------
    Private Shared Function GetDbPath() As String
        Dim strTranslationsPath As String = String.Concat(System.Windows.Forms.Application.StartupPath, "\translations")
        Dim strDbFile As String = "rInstatTranslations.db"
        Dim strDbPath As String = System.IO.Path.Combine(strTranslationsPath, strDbFile)
        Return strDbPath
    End Function

    '''--------------------------------------------------------------------------------------------
    ''' <summary>   
    '''    Writes a CSV file that can be imported into the `TranslateWinForm` library database. 
    '''    This sub should be executed prior to each release to ensure that the `TranslateWinForm` 
    '''    database contains all the translatable text for the new release.
    '''    <para>
    '''    The CSV file contains the identifiers and associated text of each form, control and menu 
    '''    item in the application.     Please note that `ucrCheck` and `ucrInput` controls are 
    '''    specifically excluded. This is because the text for these controls is set dynamically 
    '''    at runtime.
    '''    </para><para>
    '''    This sub uses the `Reflection` package to automatically identify and traverse all the 
    '''    forms, menus and controls in the current release. This information can also be found by 
    '''    parsing the application source code files (e.g. the `resx` or `xlf` files). However, 
    '''    we considered the `Reflection` package to be a simpler and less error-prone solution.
    '''    </para>
    ''' </summary>
    '''--------------------------------------------------------------------------------------------
    Private Shared Sub WriteCsvFile()

        'Get list of all form classes in the application 
        '    (specifically, a list of 'Type' objects, each 'Type' object contains details about 
        '    a class)
        Dim clsAssembly As Assembly = Assembly.GetExecutingAssembly()
        Dim lstFormClasses As List(Of Type) = clsAssembly.GetTypes().Where(Function(t) t.BaseType = GetType(Form)).ToList()

        'Populate the csv string for each form in the project
        'Note: We know the name of each form class (see list above). We also know that 
        '      the 'My.Forms' object contains an object for each form class.
        '      Conveniently, the name of each object in 'My.Forms' is the same as the name of 
        '      the object's class. 
        '      Therefore we can use the class name as the object name in 'CallByName'.
        Dim strControlsAsCsv As String = ""
        For Each typFormClass As Type In lstFormClasses
            Dim frmTemp As Form = CallByName(My.Forms, typFormClass.Name, CallType.Get)
            strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetControlsAsCsv(frmTemp)
        Next

        'The right mouse button menus for the 6 output windows are not accessible via 
        '    the control lists. Therefore, add these manually to the CSV file
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrOutput, frmMain.ucrOutput.mnuContextRTB.Items)
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrColumnMeta, frmMain.ucrColumnMeta.cellContextMenuStrip.Items)
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrColumnMeta, frmMain.ucrColumnMeta.grdVariables.RowHeaderContextMenuStrip.Items)
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrColumnMeta, frmMain.ucrColumnMeta.statusColumnMenu.Items)
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrDataFrameMeta, frmMain.ucrDataFrameMeta.cellContextMenuStrip.Items)
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrDataFrameMeta, frmMain.ucrDataFrameMeta.rowRightClickMenu.Items)
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrLogWindow, frmMain.ucrLogWindow.mnuContextLogFile.Items)
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrScriptWindow, frmMain.ucrScriptWindow.mnuContextScript.Items)
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrDataViewer, frmMain.ucrDataViewer.grdData.RowHeaderContextMenuStrip.Items)
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrDataViewer, frmMain.ucrDataViewer.grdData.ColumnHeaderContextMenuStrip.Items)
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrDataViewer, frmMain.ucrDataViewer.grdData.ContextMenuStrip.Items)
        strControlsAsCsv &= TranslateWinForm.clsTranslateWinForm.GetMenuItemsAsCsv(frmMain.ucrDataViewer, frmMain.ucrDataViewer.grdData.SheetTabContextMenuStrip.Items)

        'Write the csv file
        Dim strDesktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        Dim strFileName As String = "form_controls.csv"
        Dim strPath As String = System.IO.Path.Combine(strDesktopPath, strFileName)
        Using sw As New System.IO.StreamWriter(strPath)
            Console.WriteLine(strControlsAsCsv)
            sw.WriteLine(strControlsAsCsv)
            sw.Flush()
            sw.Close()
        End Using

        'This sub should only be used by developers to create the translation export files.
        'Therefore, exit the application with a message to ensure that this sub is not run 
        'accidentally in the release version. 
        MsgBox("The form controls' translation text was written to: " & strPath &
               ". The application will now exit.", MsgBoxStyle.Exclamation)
        System.Windows.Forms.Application.Exit()
    End Sub


    '**********************************************************************************************
    'TODO This section contains functions from the old translation system.
    ' These functions will be replaced as part of the new translation system started in March 2021.
    '**********************************************************************************************

    Public Shared Function translate(tag As String) As String
        ' Note: if the tag is not found in Resources then Nothing will be returned
        Return My.Resources.ResourceManager.GetObject(tag)
    End Function

    Public Shared Sub translateEach(controls As Control.ControlCollection, ctrParent As Control, Optional res As ComponentModel.ComponentResourceManager = Nothing, Optional CultureInfo As Globalization.CultureInfo = Nothing)
        Dim mnuTmp As MenuStrip
        Dim pntLocation As Point

        If res Is Nothing Then
            res = New ComponentModel.ComponentResourceManager(ctrParent.GetType)
        End If
        If CultureInfo Is Nothing Then
            CultureInfo = Threading.Thread.CurrentThread.CurrentUICulture
        End If
        For Each aControl As Control In controls
            'Checkbox text is set in the dialog so shouldn't be translated
            'Input controls use Text property for display value so shouldn't be translated
            If TypeOf aControl IsNot ucrCheck AndAlso TypeOf aControl IsNot ucrInput Then
                If TypeOf aControl Is MenuStrip Then
                    mnuTmp = DirectCast(aControl, MenuStrip)
                    translateMenu(mnuTmp.Items, ctrParent)
                ElseIf TypeOf aControl Is UserControl OrElse TypeOf aControl Is Panel OrElse TypeOf aControl Is GroupBox OrElse TypeOf aControl Is TabControl OrElse TypeOf aControl Is SplitContainer OrElse TypeOf aControl Is TreeView Then
                    translateEach(aControl.Controls, aControl, res, CultureInfo)
                End If
                If aControl.Name <> "" Then
                    pntLocation = aControl.Location
                    res.ApplyResources(aControl, aControl.Name, CultureInfo)
                    aControl.Location = pntLocation
                End If
            End If
        Next
    End Sub

End Class
