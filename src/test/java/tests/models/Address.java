package tests.models;

public class Address {
    private String street;
    private String suite;
    private String city;
    private String zipcode;
    private Geo geo;

    @Override
    public boolean equals(Object obj) {
        Address address = (Address) obj;
        return (street.equals(address.street) &&
                suite.equals(address.suite) &&
                city.equals(address.city) &&
                zipcode.equals(address.zipcode) &&
                geo.equals(address.geo));
    }

    @Override
    public String toString() {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(" Address{ ");
        stringBuilder.append("street=" + street + " ");
        stringBuilder.append("suite=" + suite + " ");
        stringBuilder.append("city=" + city + " ");
        stringBuilder.append("zipcode=" + zipcode + " ");
        stringBuilder.append("geo=" + geo);
        stringBuilder.append("}");
        return stringBuilder.toString();
    }

    public Geo getGeo() {
        return geo;
    }

    public String getCity() {
        return city;
    }

    public String getStreet() {
        return street;
    }

    public String getSuite() {
        return suite;
    }

    public String getZipcode() {
        return zipcode;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public void setGeo(Geo geo) {
        this.geo = geo;
    }

    public void setStreet(String street) {
        this.street = street;
    }

    public void setSuite(String suite) {
        this.suite = suite;
    }

    public void setZipcode(String zipcode) {
        this.zipcode = zipcode;
    }
}
