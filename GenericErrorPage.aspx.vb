
Partial Class GenericErrorPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        HttpContext.Current.Response.StatusCode = 500
        Dim hostName = System.Net.Dns.GetHostName()
        Dim ctxOBJ As HttpContext = HttpContext.Current()
        Dim exceptionOBJ As Exception = Nothing

        If ctxOBJ IsNot Nothing Then
            exceptionOBJ = ctxOBJ.Server.GetLastError()
        End If

        Dim errorInfoTXT As String = " <br>Offending URL: " & If(ctxOBJ Is Nothing, "ei saatavilla", ctxOBJ.Request.Url.ToString()) & _
        "<br>Source: " & If(exceptionOBJ Is Nothing, "ei saatavilla", exceptionOBJ.Source.ToString()) & _
        "<br>Message: " & If(exceptionOBJ Is Nothing, "ei saatavilla", exceptionOBJ.Message.ToString()) & _
        "<br>Stack trace: " & If(exceptionOBJ Is Nothing, "ei saatavilla", exceptionOBJ.StackTrace.ToString()) & _
        "<br>Target Site: " & If(exceptionOBJ Is Nothing, "ei saatavilla", exceptionOBJ.TargetSite.ToString()) & _
        "<br>Server: " & hostName

        lblErrorMsg.Text = errorInfoTXT
    End Sub
End Class
