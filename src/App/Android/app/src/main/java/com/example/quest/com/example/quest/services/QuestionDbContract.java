package com.example.quest.com.example.quest.services;

import android.provider.BaseColumns;

public final class QuestionDbContract {
    public QuestionDbContract(){ }

    public static class QuestionModel implements BaseColumns {
        public static final String Table_Name = "question";
        public static final String Column_Id = "id";
        public static final String Column_Value = "value";
        public static final String Column_Answer = "answer";
        public static final String Column_Level = "level";
    }
}
