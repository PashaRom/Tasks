package tests.models;

public class Company {
    private String name;
    private String catchPhrase;
    private String bs;

    @Override
    public boolean equals(Object obj) {
        Company company = (Company) obj;
        return (name.equals(company.name) &&
                catchPhrase.equals(company.catchPhrase) &&
                bs.equals(company.bs));
    }

    @Override
    public String toString() {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(" Company{ ");
        stringBuilder.append("name=" + name + " ");
        stringBuilder.append("catchPhrase=" + catchPhrase + " ");
        stringBuilder.append("bs=" + bs);
        stringBuilder.append("}");
        return stringBuilder.toString();
    }

    public String getName() {
        return name;
    }

    public String getBs() {
        return bs;
    }

    public String getCatchPhrase() {
        return catchPhrase;
    }

    public void setBs(String bs) {
        this.bs = bs;
    }

    public void setCatchPhrase(String catchPhrase) {
        this.catchPhrase = catchPhrase;
    }

    public void setName(String name) {
        this.name = name;
    }
}

