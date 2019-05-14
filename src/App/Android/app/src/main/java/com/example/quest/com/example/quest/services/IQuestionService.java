package com.example.quest.com.example.quest.services;

import com.example.quest.com.example.quest.models.Question;

import java.util.List;

public interface IQuestionService {
    List<Question> getAllQuestions();
    Question getQuestion(String questionId);
}
