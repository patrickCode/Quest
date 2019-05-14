package com.example.quest.com.example.quest.models;

import java.util.Date;

public class Question {
    String id;
    String value;
    String answerTypeCode;
    String level;
    String questionTypeCode;
    //Category[] categories;
    Date lastModifiedOn;
    String lastModifiedBy;
    String[] tags;
    Boolean isPrivate;
    String createdBy;
    String correctAnswer;

    public Question(String id, String value, String answer, String level) {
        this.id = id;
        this.value = value;
        this.correctAnswer = answer;
        this.level = level;
    }

    public String getId() { return this.id; }

    public String getQuestion() {
        return this.value;
    }

    public String getAnswer() {
        return this.correctAnswer;
    }

    public String getLevel(){
        return this.level;
    }
}