<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeCrud.aspx.cs" Inherits="Crud.EmployeeCrud" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Management System</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css" />
 
</head>
<body class="bg-light">
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
        <div class="container">
            <a class="navbar-brand" href="#">
                <i class="fas fa-users-cog me-2"></i>Employee Management System
            </a>
        </div>
    </nav>

    <div class="container mt-4">
        <div class="row">
            <div class="col-md-12">
                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0"><i class="fas fa-user-plus me-2"></i>Employee Information</h4>
                    </div>
                    <div class="card-body">
                        <form id="form1" runat="server">
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label class="form-label">Employee Name</label>
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter employee name"></asp:TextBox>
                                        <asp:Label ID="lblError" runat="server" CssClass="text-danger small" />
                                    </div>
                                    <div class="mb-3">
                                        <label class="form-label">Gender</label>
                                        <div>
                                            <asp:RadioButton ID="rbMale" runat="server" GroupName="Gender" Text="Male" CssClass="form-check-inline me-2" />
                                            <asp:RadioButton ID="rbFemale" runat="server" GroupName="Gender" Text="Female" CssClass="form-check-inline" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label class="form-label">Department</label>
                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-select">
                                            <asp:ListItem Value="0">Select Department</asp:ListItem>
                                            <asp:ListItem Value="IT">IT</asp:ListItem>
                                            <asp:ListItem Value="HR">HR</asp:ListItem>
                                            <asp:ListItem Value="Finance">Finance</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="mb-3">
                                        <label class="form-label">Status</label>
                                        <div>
                                            <asp:CheckBox ID="chkActive" runat="server" Text="Is Active" CssClass="form-check-label" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="d-flex gap-2">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-warning" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btnClear_Click" />
                            </div>

                            <hr class="my-4" />

                            <div class="row mb-3">
                                <div class="col-md-8">
                                    <h5><i class="fas fa-search me-2"></i>Find Employees</h5>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by name..." />
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-hover table-striped"
                                    DataKeyNames="Id" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    HeaderStyle-CssClass="table-primary">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="ID" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <%# (bool)Eval("IsActive") ? "<span class='badge bg-success'>Active</span>" : "<span class='badge bg-secondary'>Inactive</span>" %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Department" HeaderText="Department" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <a href='EmployeeCrud.aspx?Id=<%# Eval("Id") %>&Action=Edit' class="btn btn-sm btn-info">
                                                        <i class="fas fa-edit"></i> Edit
                                                    </a>
                                                    <a href='EmployeeCrud.aspx?Id=<%# Eval("Id") %>&Action=Delete' class="btn btn-sm btn-danger"
                                                       onclick="return confirm('Are you sure you want to delete this employee?');">
                                                        <i class="fas fa-trash"></i> Delete
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <footer class="mt-5 mb-3 text-center text-muted">
            <small>&copy; 2025 Employee Management System. All rights reserved.</small>
        </footer>
    </div>
</body>
</html>