<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="ResumeBuild.aspx.cs" Inherits="EmploymentAgency.User.ResumeBuild" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <div class="container pt-50 pb-40">
            <div class="row">
                <div class="col-12 pb-20">
                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="col-12">
                    <h2 class="contact-title text-center">Build Resume</h2>
                </div>
                <div class="col-lg-12">
                    <div class="form-contact contact_form">
                        <div class="row">
                            <div class="col-12">
                                <h6>Personal Information</h6>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Full Name</label>
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter Full Name" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Name must be in characters" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txtFullName"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter Unique Username" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Address</label>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Phone Number</label>
                                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" placeholder="Enter Phone Number" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Phone Number must have 10 digits!" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" ValidationExpression="^[0-9]{10}$" ControlToValidate="txtPhoneNumber"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" required TextMode="Email"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <!-- Block‑level label with bottom margin -->
                                    <label for="ddlCity" class="d-block mb-1">City</label>

                                    <!-- Dropdown styled exactly like your textboxes -->
                                    <asp:DropDownList
                                        ID="ddlCity"
                                        runat="server"
                                        CssClass="form-control w-100"
                                        DataSourceID="SqlDataSource1"
                                        DataTextField="CityName"
                                        DataValueField="CityName"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="Select City" />
                                    </asp:DropDownList>

                                    <!-- Validator stays right under the dropdown -->
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1"
                                        runat="server"
                                        ControlToValidate="ddlCity"
                                        InitialValue="0"
                                        ErrorMessage="City is required"
                                        CssClass="text-danger"
                                        Display="Dynamic" />

                                    <!-- Your data source as before -->
                                    <asp:SqlDataSource
                                        ID="SqlDataSource1"
                                        runat="server"
                                        ConnectionString="<%$ ConnectionStrings:EmploymentAgencyConnectionString %>"
                                        ProviderName="<%$ ConnectionStrings:EmploymentAgencyConnectionString.ProviderName %>"
                                        SelectCommand="SELECT [CityName] FROM [City]" />
                                </div>
                            </div>


                            <div class="col-12 pt-4">
                                <h6>Education / Resume Information</h6>
                            </div>

                            <!-- Education Fields -->
                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>10th Grade</label>
                                    <asp:TextBox ID="txtTenth" runat="server" CssClass="form-control" placeholder="Ex: 3.5 gpa" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>12th Grade</label>
                                    <asp:TextBox ID="txtTwelveth" runat="server" CssClass="form-control" placeholder="Ex: 3.5 gpa" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Graduation Grade</label>
                                    <asp:TextBox ID="txtGraduation" runat="server" CssClass="form-control" placeholder="Ex: 3.5 GPA" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Post Graduation Grade</label>
                                    <asp:TextBox ID="txtPostGraduation" runat="server" CssClass="form-control" placeholder="Ex: 3.5 GPA" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>PHD Grade</label>
                                    <asp:TextBox ID="txtPhd" runat="server" CssClass="form-control" placeholder="Ex: PHD Grade" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Job Profile</label>
                                    <asp:TextBox ID="txtWork" runat="server" CssClass="form-control" placeholder="Job Profile" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Work Experience</label>
                                    <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" placeholder="Work Experience" required></asp:TextBox>
                                </div>
                            </div>

                            <!-- File Upload -->
                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Resume</label>
                                    <asp:FileUpload ID="fuResume" runat="server" CssClass="form-control" ToolTip=".doc, .docx,. pdf extension only" />
                                </div>
                            </div>

                            <div class="form-group mt-3 text-center">
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button button-contactForm boxed-btn mr-4" OnClick="btnUpdate_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
