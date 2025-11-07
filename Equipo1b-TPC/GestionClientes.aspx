<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="GestionClientes.aspx.cs" Inherits="Equipo1b_TPC.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upGestionClientes" runat="server">
        <ContentTemplate>
            <div class="container mt-4">
                <div class="card-grid p-4 border-1 text-white" style="min-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
                    <div class="row justify-content-center align-items-center">
                        <div class="col-auto">
                            <asp:Button ID="btnAgregarCliente" CssClass="btn btn-primary" runat="server" OnClick="btnAgregarCliente_Click" Text="Agregar nuevo cliente" />
                        </div>
                        <div class="col-auto">
                            <asp:CheckBox ID="chkCuit" Text="Filtrar por cuit" CssClass="form-check" runat="server" />
                        </div>
                        <div class="col-auto">
                            <asp:CheckBox ID="ChkRazonSocial" Text="filtrar por razon social" CssClass="form-check" runat="server" />
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
                    <div class="col-12 mt-2">
                        <asp:GridView ID="gvClientes" ClientIDMode="Static" ShowHeaderWhenEmpty="true" EmptyDataText="No se encontraron clientes en la base de datos" CssClass="table table-striped table-bordered text-center table-hover w-100" AutoGenerateColumns="false" runat="server">
                            <Columns>
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
                                        <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-primary mr-1" OnClick="btnEditar_Click" Text="Editar" CommandArgument='<%# Eval("cuit") %>' />
                                        <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger" OnClick="btnEliminar_Click" Text="Eliminar" CommandArgument='<%# Eval("cuit") %>' Visible='<%# Convert.ToBoolean(Eval("Activo")) %>' />
                                        <asp:Button ID="btnActivar" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="btnActivar_Click" Text="Activar" CommandArgument='<%# Eval("cuit") %>' Visible='<%# !Convert.ToBoolean(Eval("Activo")) %>' />
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
