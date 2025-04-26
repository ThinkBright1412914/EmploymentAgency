<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="EmploymentAgency.Admin.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-size: cover; background-repeat: no-repeat; background-position: center;">
        <div class="container-fluid pt-4 pb-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            <h3 class="text-center">User List/Details</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" HHeaderStyle-HorizontalAlign ="Center"
                        EmptyDataText="No Record to Display...!" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                        OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="UserID" OnRowDeleting="GridView1_RowDeleting">
                        <Columns>

                            <asp:BoundField DataField="S.No" HeaderText="S.No">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Name" HeaderText="Username">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PhoneNumber" HeaderText="Subject">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="City" HeaderText="Message">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:CommandField CausesValidation="False" HeaderText="Delete" ShowDeleteButton="True"
                                DeleteImageUrl="../assets/img/icon/trashIcon.png" ButtonType="Image">
                                <ControlStyle Height="25px" Width="25px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle BackColor="#7200cf" ForeColor="White" />
                    </asp:GridView>


                </div>
            </div>
        </div>
    </div>

</asp:Content>
