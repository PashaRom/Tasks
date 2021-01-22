package tests.models;

public class Geo {
    private String lat;
    private String lng;

    @Override
    public boolean equals(Object obj) {
        Geo geo = (Geo) obj;
        return lat.equals(geo.lat) && lng.equals(geo.lng);
    }

    @Override
    public String toString() {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(" Geo{ ");
        stringBuilder.append("lat=" + lat + " ");
        stringBuilder.append("lat=" + lng);
        stringBuilder.append("}");
        return stringBuilder.toString();
    }

    public String getLat() {
        return lat;
    }

    public String getLng() {
        return lng;
    }

    public void setLat(String lat) {
        this.lat = lat;
    }

    public void setLng(String lng) {
        this.lng = lng;
    }
}
