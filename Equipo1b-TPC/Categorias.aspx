<%@ Page Title="Gesti�n de Categor�as" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="Equipo1b_TPC.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Gesti�n de Categor�as</h2>
        
        <div class="row mt-3">
            <div class="col-12">
                <asp:GridView ID="dgvCategorias" runat="server" 
                    CssClass="table table-striped table-bordered" 
                    AutoGenerateColumns="false"
                    DataKeyNames="Id"
                    OnSelectedIndexChanged="dgvCategorias_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripci�n" />
                        <asp:CheckBoxField DataField="Activo" HeaderText="Activo" />
                        <asp:CommandField ShowSelectButton="true" SelectText="Editar" HeaderText="Acciones" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h5>Agregar/Modificar Categor�a</h5>
                    </div>
                    <div class="card-body">
                        <asp:HiddenField ID="hfIdCategoria" runat="server" Value="0" />
                        
                        <div class="mb-3">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblDescripcion" runat="server" Text="Descripci�n:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500"></asp:TextBox>
                        </div>

                        <div class="mb-3 form-check">
                            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input" Checked="true" />
                            <asp:Label ID="lblActivo" runat="server" Text="Activo" CssClass="form-check-label"></asp:Label>
                        </div>

                        <div class="d-flex gap-2">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-info mt-3" Visible="false"></asp:Label>
    </div>
</asp:Content>
