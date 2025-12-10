<%@ Page Title="Gestión de Marcas" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="Equipo1b_TPC.Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="card-grid p-4 border-1 text-white" style="min-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
            <h1 class="text-center text-white fw-bold text-primary">Gestion de Marcas
            </h1>
            <div class="row mt-3">
                <div class="col-12 tabla-stock">
                    <asp:GridView ID="dgvMarcas" runat="server"
                        CssClass="table table-striped table-bordered"
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        OnSelectedIndexChanged="dgvMarcas_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:CheckBoxField DataField="Activo" HeaderText="Activo" />
                            <asp:CommandField ShowSelectButton="true" SelectText="Editar" HeaderText="Acciones" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class="container mt-4">
        <div class="card-grid p-4 border-1 text-white" style="max-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
            <h5>Agregar/Modificar Marca</h5>
            <div class="card-body">
                <asp:HiddenField ID="hfIdMarca" runat="server" Value="0" />

                <div class="mb-3">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>

                <div class="mb-3 form-check">
                    <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
                    <asp:Label ID="lblActivo" runat="server" Text="Activo" CssClass="form-check-label"></asp:Label>
                </div>

                <div class="d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                </div>
            </div>
            <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-info mt-3" Visible="false"></asp:Label>
        </div>
    </div>




</asp:Content>
