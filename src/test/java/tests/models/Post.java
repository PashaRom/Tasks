package tests.models;

public class Post {
    private int userId;
    private int id;
    private String title;
    private String body;
    public int getUserId(){
        return userId;
    }

    public int getId(){
        return id;
    }

    public String getTitle(){
        return title;
    }

    public String getBody(){
        return body;
    }

    public void setId(int id){
        this.id = id;
    }

    public void setUserId(int userId){
        this.userId = userId;
    }

    public void setTitle(String title){
        this.title = title;
    }

    public void setBody(String body){
        this.body = body;
    }

    @Override
    public String toString() {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("Post{");
        stringBuilder.append("id=" + id + " ");
        stringBuilder.append("userId=" + userId + " ");
        stringBuilder.append("title=" + title + " ");
        stringBuilder.append("body=" + body);
        stringBuilder.append("}");
        return super.toString();
    }
}
