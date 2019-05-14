package com.example.quest;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import java.util.List;

import com.example.quest.com.example.quest.models.Question;

public class QuestionsAdapter extends RecyclerView.Adapter<QuestionsAdapter.QuestionViewHolder> {
    private Context _context;
    private  List<Question> _questionList;

    public class QuestionViewHolder extends RecyclerView.ViewHolder {
        public TextView questionView, answerView, levelView;
        public ImageView overflow;

        public QuestionViewHolder(View view) {
            super(view);
            questionView = (TextView) view.findViewById(R.id.question_value);
            answerView = (TextView) view.findViewById(R.id.question_answer);
            levelView = (TextView) view.findViewById(R.id.question_level);
            overflow = (ImageView) view.findViewById(R.id.question_overflow);
        }
    }

    public QuestionsAdapter(Context context, List<Question> questionList) {
        this._context = context;
        this._questionList = questionList;
    }

    @Override
    public QuestionViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        View questionView = LayoutInflater.from(viewGroup.getContext())
                .inflate(R.layout.question_card, viewGroup, false);

        return new QuestionViewHolder(questionView);
    }

    @Override
    public void onBindViewHolder(@NonNull QuestionViewHolder questionViewHolder, int position) {
        Question question = _questionList.get(position);
        questionViewHolder.questionView.setText(question.getQuestion());
        questionViewHolder.answerView.setText(question.getAnswer());
        questionViewHolder.levelView.setText(question.getLevel());

        questionViewHolder.overflow.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Toast.makeText(_context,"Clicked", Toast.LENGTH_SHORT).show();
                //GO to details here
            }
        });
    }

    @Override
    public int getItemCount() {
        return _questionList.size();
    }
}
