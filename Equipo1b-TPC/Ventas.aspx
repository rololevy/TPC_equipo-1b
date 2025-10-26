<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="Equipo1b_TPC.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upClientes" runat="server">
        <ContentTemplate>
            <div class="container mt-4">
                <div class="card p-4 border-1 text-white" style="min-width:400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
                        <div class="row justify-content-center align-items-center">
                            <div class="col-5">
                                <asp:TextBox ID="txtFiltrarClientes" AutoPostBack="true" OnTextChanged="txtFiltrarClientes_TextChanged" placeHolder="Buscar Cliente ......" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-5">
                                <asp:DropDownList ID="ddlClientes" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-2">
                                <asp:Button ID="btnAgregarCliente" runat="server" CssClass="btn btn-primary" OnClick="btnAgregarCliente_Click" Text="Agregar Nuevo cliente" />
                            </div>
                        </div>
                        <div class="row justify-content-center align-items-start mt-3">
                            <div class="col-12">
                                <asp:GridView ID="gvProductos" ClientIDMode="Static" ShowHeaderWhenEmpty="true" EmptyDataText="no hay articulos cargados para realizar una venta" CssClass="table table-striped table-bordered text-center w-100" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="codigo" />
                                        <asp:BoundField DataField="descripcion" HeaderText="descripcion" />
                                        <asp:BoundField DataField="cantidad" HeaderText="canitdad" />
                                        <asp:BoundField DataField="precio" HeaderText="precio" />
                                        <asp:BoundField DataField="total" HeaderText="total" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row justify-content-center align-items-center">
                            <div class="col-5">
                                <asp:TextBox ID="txtIdProducto" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtIdProducto_TextChanged" placeHolder="id Producto" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-5">
                                <asp:DropDownList ID="ddlProductos" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-2">
                                <asp:Button ID="btnAgregarProducto" CssClass="btn btn-primary" runat="server" Text="agregar producto" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

