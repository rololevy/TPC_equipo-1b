<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="GestionProveedores.aspx.cs" Inherits="Equipo1b_TPC.GestionProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upGestionProveedores" runat="server">
    <ContentTemplate>
        <div class="container mt-4">
            <div class="card-grid p-4 border-1 text-white" style="min-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
                <div class="row justify-content-center align-items-center">
                    <div class="col-auto">
                        <asp:Button ID="btnAgregarProveedor" CssClass="btn btn-primary" runat="server" OnClick="btnAgregarProveedor_Click" Text="Agregar nuevo proveedor" />
                    </div>
                    <div class="col-auto">
                        <asp:CheckBox ID="chkCuit" AutoPostBack="true" Text="Filtrar por cuit" CssClass="form-check" runat="server" />
                    </div>
                    <div class="col-auto">
                        <asp:CheckBox ID="ChkRazonSocial" AutoPostBack="true" Text="filtrar por razon social" CssClass="form-check" runat="server" />
                    </div>
                    <div class="col-auto">
                        <asp:TextBox ID="txtFiltro" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-auto">
                        <asp:Button ID="btnLimpiarFiltros" CssClass="btn btn-primary" OnClick="btnLimpiarFiltros_Click" runat="server" Text="Limpiar Filtros" />
                    </div>
                </div>
                <div class="row justify-content-center align-items-center text-center">
                    <asp:Label ID="lblFiltro" runat="server" ClientIDMode="Static" CssClass="text-danger fw-bold text-center" Visible="false"></asp:Label>
                </div>
                <div class="col-12 mt-2 tabla-stock">
                    <asp:GridView ID="gvProveedores" ClientIDMode="Static" ShowHeaderWhenEmpty="true" EmptyDataText="No se encontraron clientes en la base de datos" CssClass="table table-striped table-bordered text-center table-hover w-100" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:BoundField  DataField="ID" HeaderText="ID" Visible="false"/>
                            <asp:BoundField DataField="razonSocial" HeaderText="razon social" />
                            <asp:BoundField DataField="cuit" HeaderText="cuit" />
                            <asp:BoundField DataField="email" HeaderText="email" />
                            <asp:BoundField DataField="telefono" HeaderText="telefono" />
                            <asp:BoundField DataField="direccion" HeaderText="direccion" />
                            <asp:TemplateField HeaderText="Activo">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActivo" runat="server" Checked='<%# Convert.ToBoolean(Eval("Activo")) %>' Enabled="false" CssClass="form-check-input" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-primary mr-1" OnClick="btnEditar_Click" Text="Editar" CommandArgument='<%# Eval("ID") %>' />
                                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger" OnClick="btnEliminar_Click" Text="Desactivar" CommandArgument='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("Activo")) %>' />
                                    <asp:Button ID="btnActivar" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="btnActivar_Click" Text="Activar" CommandArgument='<%# Eval("ID") %>' Visible='<%# !Convert.ToBoolean(Eval("Activo")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
