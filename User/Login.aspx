<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EmploymentAgency.User.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class=" container pt-50 pb-40">
            <div class="row">
                <div class="col-12 pb-20">
                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="col-12">
                    <h2 class="contact-title text-center">Sign In</h2>
                </div>
                <div class="col-lg-6 mx-auto">
                    <div class="form-contact contact_form">
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control w-100" placeholder="Enter Unique Username" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Password</label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control w-100" placeholder="Enter Password"
                                        TextMode="Password" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Login Type</label>
                                    <asp:DropDownList ID="ddlLoginType" runat="server" CssClass="form-control w-100">
                                    <asp:ListItem Value="0">Select Login Type</asp:ListItem>
                                    <asp:ListItem>Admin</asp:ListItem>
                                    <asp:ListItem>User</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="User is required"
                                        ForeColor="Red" Display="Dynamic" Font-Size="Small" InitialValue="0"
                                        ControlToValidate="ddlLoginType"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group mt-3">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="button button-contactForm boxed-btn mr-4" OnClick="btnLogin_Click" />
                            <span class="clickLink"><a href="../User/Register.aspx">New User? Click Here...</a></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
