<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Equipo1b_TPC.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Error</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
            padding-top: 50px;
        }
        .error-container {
            max-width: 700px;
            margin: 0 auto;
            background-color: white;
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0,0,0,0.1);
        }
        .error-icon {
            font-size: 80px;
            text-align: center;
            margin-bottom: 20px;
        }
        .error-title {
            color: #dc3545;
            text-align: center;
            margin-bottom: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="error-container">
                <div class="error-icon">??</div>
                <h2 class="error-title">Ha ocurrido un error</h2>
                
                <div class="alert alert-danger" role="alert">
                    <asp:Label ID="lblErrorMessage" runat="server" Text="Ha ocurrido un error inesperado."></asp:Label>
                </div>

                <div class="alert alert-info" role="alert">
                    <strong>�Qu� hacer?</strong>
                    <ul class="mb-0 mt-2">
                        <li>Verifica los datos ingresados</li>
                        <li>Intenta nuevamente</li>
                        <li>Contacta al administrador si persiste</li>
                    </ul>
                </div>

                <div class="d-grid gap-2 mt-4">
                    <asp:Button ID="btnVolver" runat="server" Text="Volver al Inicio" 
                        CssClass="btn btn-primary" OnClick="btnVolver_Click" />
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" 
                        CssClass="btn btn-secondary" OnClick="btnRegresar_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
