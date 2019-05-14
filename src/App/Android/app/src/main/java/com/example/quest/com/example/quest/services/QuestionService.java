package com.example.quest.com.example.quest.services;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.provider.BaseColumns;

import com.example.quest.com.example.quest.models.Question;

import java.util.ArrayList;
import java.util.List;

public class QuestionService implements IQuestionService {

    private QuestionSqlHelper _repository;

    public QuestionService(Context context){
        _repository = new QuestionSqlHelper(context);
    }

    public List<Question> getAllQuestions(){
        List<Question> questions = new ArrayList<>();

        SQLiteDatabase db = _repository.getReadableDatabase();
        String[] project = {
                QuestionDbContract.QuestionModel.Column_Id,
                QuestionDbContract.QuestionModel.Column_Value,
                QuestionDbContract.QuestionModel.Column_Answer,
                QuestionDbContract.QuestionModel.Column_Level
        };

        Cursor questionsCursor = db.query(QuestionDbContract.QuestionModel.Table_Name, project, null, null, null, null, null);
        while(questionsCursor.moveToNext()) {
            String questionId = questionsCursor.getString(questionsCursor.getColumnIndex(QuestionDbContract.QuestionModel.Column_Id));
            String questionValue = questionsCursor.getString(questionsCursor.getColumnIndex(QuestionDbContract.QuestionModel.Column_Value));
            String questionAnswer = questionsCursor.getString(questionsCursor.getColumnIndex(QuestionDbContract.QuestionModel.Column_Answer));
            String questionLevel = questionsCursor.getString(questionsCursor.getColumnIndex(QuestionDbContract.QuestionModel.Column_Level));

            questions.add(new Question(questionId, questionValue, questionAnswer, questionLevel));
        }
        questionsCursor.close();

        return questions;
    }

    public Question getQuestion(String questionId) {
        return new Question("3", "What is a question 3?", "This is an answer 3", "Advanced");
    }

    public void AddQuestion(Question question) {
        SQLiteDatabase db = _repository.getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(QuestionDbContract.QuestionModel.Column_Id, question.getId());
        values.put(QuestionDbContract.QuestionModel.Column_Value, question.getQuestion());
        values.put(QuestionDbContract.QuestionModel.Column_Answer, question.getAnswer());
        values.put(QuestionDbContract.QuestionModel.Column_Level, question.getLevel());
        db.insert(QuestionDbContract.QuestionModel.Table_Name, null, values);
    }

    public void AddDummyQuestions(){
        SQLiteDatabase db = _repository.getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(QuestionDbContract.QuestionModel.Column_Id, "1");
        values.put(QuestionDbContract.QuestionModel.Column_Value, "What is Android?");
        values.put(QuestionDbContract.QuestionModel.Column_Answer, "Android is an Operating System used for Mobile Development");
        values.put(QuestionDbContract.QuestionModel.Column_Level, "Basic");
        db.insert(QuestionDbContract.QuestionModel.Table_Name, null, values);

        values.clear();
        values.put(QuestionDbContract.QuestionModel.Column_Id, "2");
        values.put(QuestionDbContract.QuestionModel.Column_Value, "What kind of databases are used in Android");
        values.put(QuestionDbContract.QuestionModel.Column_Answer, "SQL DB");
        values.put(QuestionDbContract.QuestionModel.Column_Level, "Basic");
        db.insert(QuestionDbContract.QuestionModel.Table_Name, null, values);
    }

    public void CleanCache() {
        String deleteCmd = "DELETE FROM " + QuestionDbContract.QuestionModel.Table_Name;
        SQLiteDatabase db = _repository.getWritableDatabase();
        db.execSQL(deleteCmd);
    }
}
