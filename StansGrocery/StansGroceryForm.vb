'Sebastian Soto
'RCET0265
'Fall 2021
'Stan's Grocery
'https://github.com/SebastianSotoMk4/StansGrocery.git
'Instructinos 
'https://github.com/rosstimo/StansGrocery
Option Strict On
Option Explicit On
Public Class StansGroceryForm
    Dim fileName As String = "..\..\Grocery.txt"
    Dim groceryData(199, 3) As String
    Dim tempList As New List(Of String)

    Sub SampleTxt()
        Dim currentRecord As String
        FileOpen(1, "Grocery.txt", OpenMode.Input)
        Do Until EOF(1)
            Input(1, currentRecord)
            ProductListBox.Text = currentRecord
            'Console.WriteLine(currentRecord)
        Loop
        FileClose(1)
    End Sub
    'sets defaults when loaded or called
    Sub Defaults()
        AisleRadioButton.Checked = True
    End Sub
    'Needs to ask to open a file
    Sub ReadFile()
        'Clear listbox each time called for neatness
        ProductListBox.Items.Clear()
        Dim currentLine As String
        Dim temp() As String
        Dim itemNo As Integer
        Try
            FileOpen(1, Me.fileName, OpenMode.Input)
            Do Until EOF(1)
                For lineNumber = 0 To 3
                    Input(1, currentLine)
                    Me.groceryData(itemNo, lineNumber) = currentLine
                    FileClose(1)
                Next
                itemNo += 1
            Loop
        Catch ex As Exception
            ' ask to choose a file 
            MsgBox($"no file ???")
        End Try
    End Sub

    Function SeperateRecords(ByVal record As String) As String

    End Function

    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        ReadFile()
    End Sub
End Class
' How do i Open a fileDialog?? previous methods are not working.
' 