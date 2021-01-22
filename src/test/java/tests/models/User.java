package tests.models;

import com.fasterxml.jackson.annotation.JsonSetter;

public class User {
    private int id;
    private String name;
    private String userName;
    private String email;
    private Address address;
    private String phone;
    private String website;
    private Company company;

    @Override
    public boolean equals(Object obj) {
        User user = (User) obj;
        return (id == user.id &&
                name.equals(user.name) &&
                userName.equals(user.userName) &&
                email.equals(user.email) &&
                address.equals(user.address) &&
                phone.equals(user.phone) &&
                website.equals(user.website) &&
                company.equals(user.company));
    }

    @Override
    public String toString() {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(" User{");
        stringBuilder.append("id=" + id + " ");
        stringBuilder.append("name=" + name + " ");
        stringBuilder.append("userName=" + userName + " ");
        stringBuilder.append("email=" + email + " ");
        stringBuilder.append("address=" + address + " ");
        stringBuilder.append("phone=" + phone + " ");
        stringBuilder.append("website=" + website + " ");
        stringBuilder.append("company=" + company);
        stringBuilder.append("}");
        return stringBuilder.toString();
    }

    public void setId(int id){
        this.id = id;
    }

    public void setName(String name){
        this.name = name;
    }
    @JsonSetter("username")
    public void setUserName(String userName) {
        this.userName = userName;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public void setAddress(Address address) {
        this.address = address;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public void setWebsite(String website) {
        this.website = website;
    }

    public void setCompany(Company company) {
        this.company = company;
    }

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public String getUserName() {
        return userName;
    }

    public String getEmail() {
        return email;
    }

    public Address getAddress() {
        return address;
    }

    public String getPhone() {
        return phone;
    }

    public String getWebsite() {
        return website;
    }

    public Company getCompany() {
        return company;
    }
}
