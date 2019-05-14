package com.example.quest.com.example.quest.services;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class QuestionSqlHelper extends SQLiteOpenHelper {
    private static String SQL_CREATE_TABLE =
            "CREATE TABLE " + QuestionDbContract.QuestionModel.Table_Name + "(" +
                    QuestionDbContract.QuestionModel.Column_Id + " TEXT," +
                    QuestionDbContract.QuestionModel.Column_Value + " TEXT," +
                    QuestionDbContract.QuestionModel.Column_Answer + " TEXT," +
                    QuestionDbContract.QuestionModel.Column_Level + " TEXT)";

    private static String SQL_DELETE_TABLE =
            "DROP TABLE IF EXISTS " + QuestionDbContract.QuestionModel.Table_Name;

    public static final int DATABASE_VERSION = 1;
    public static final String DATABASE_NAME = "Question.db";

    public QuestionSqlHelper(Context context){
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
    }

    public void onCreate(SQLiteDatabase db){
        db.execSQL(SQL_CREATE_TABLE);
    }

    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL(SQL_DELETE_TABLE);
        onCreate(db);
    }

    public void onDowngrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        onUpgrade(db, oldVersion, newVersion);
    }
}
