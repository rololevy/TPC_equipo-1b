<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="HistorialCompras.aspx.cs" Inherits="Equipo1b_TPC.WebForm8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="card-grid p-4 text-white" style="min-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
            <asp:UpdatePanel ID="upHistorialCompras" runat="server">
                <ContentTemplate>
                    <h4 class="text-white text-center">Historial de Compras - Selecione una fecha por la cual filtrar</h4>
                    <div class="row justify-content-center align-items-center">
                        <div class="col-4 text-center">
                            <asp:Label ID="lblDesde" runat="server" CssClass="form-label" Text="Desde"></asp:Label>
                            <asp:TextBox ID="txtFechaDesde" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-4 text-center">
                            <asp:Label ID="lblHasta" runat="server" Text="Hasta" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtFechaHasta" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-4 mt-4">
                            <asp:Button ID="btnFiltrar" OnClick="btnFiltrar_Click" CssClass="btn btn-primary" runat="server" Text="Filtrar por fecha" />
                            <asp:Button ID="btnLimpiarFiltros" OnClick="btnLimpiarFiltros_Click" CssClass="btn btn-danger" runat="server" Text="limpiar filtros" />
                        </div>
                    </div>
                    <div style="max-height: 400px; overflow-y: auto; overflow-x: hidden; background: white; margin-top: 12px;">
                        <asp:GridView ID="gvHistorialCompras"  CssClass="table table-striped table-bordered text-center w-100" EmptyDataText="No hay compras historicas almacenadas" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="NroCompra" />
                                <asp:BoundField DataField="Proveedor.Id" HeaderText="Id Provedor" />
                                <asp:BoundField DataField="FechaCompra" HeaderText="Fecha de la compra"/>
                                <asp:BoundField DataField="Total" HeaderText="Total Compra"/>
                                <asp:TemplateField HeaderText="Detalle">
                                    <ItemTemplate>
                                        <asp:Button ID="btnVerDetalle" runat="server" CssClass="btn btn-sm btn-outline-primary mr-1" OnClick="btnVerDetalle_Click" Text="Ver detalle" CommandArgument='<%# Eval("Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="d-flex justify-content-center align-items-center">
                        <asp:Label ID="lblMensajeHistorial" CssClass="h4 text-danger fw-bold text-center" Visible="false" runat="server" Text=""></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
