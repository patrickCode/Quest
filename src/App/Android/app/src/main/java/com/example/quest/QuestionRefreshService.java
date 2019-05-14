package com.example.quest;

import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.os.IBinder;
import android.util.Log;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.quest.com.example.quest.models.Question;
import com.example.quest.com.example.quest.services.QuestionService;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Timer;
import java.util.TimerTask;

public class QuestionRefreshService extends Service {
    public int Counter = 0;
    private QuestionService _questionService;
    private RequestQueue _queue;

    public QuestionRefreshService(Context applicationContext) {
        super();
        Log.i("BG-Service", "Service constructor has started!");
    }

    public  QuestionRefreshService(){ }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        super.onStartCommand(intent, flags, startId);
        Log.i("BG-Service", "onStartCommand of service has been called.");
        startTimer();
        _queue = Volley.newRequestQueue(this.getApplicationContext());
        _questionService = new QuestionService(this.getApplicationContext());
        Log.i("BG-Service", "Queue has been created");
        return START_STICKY;
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        Log.i("BG-Service", "Service has been destroyed");
        //Intent broadcastIntent = new Intent(this, )
        stopTimerTask();
    }

    private Timer timer;
    private TimerTask timerTask;
    long oldTime = 0;
    public void startTimer() {
        timer = new Timer();
        initializeTimerTask();
        timer.schedule(timerTask, 10000, 10000);
    }

    public void initializeTimerTask() {
        timerTask = new TimerTask() {
            public void run() {
                Log.i("BG-Service", "In Timer++" + (Counter++) );
                String url = "http://questhunt.azurewebsites.net/api/questions?publicVisibility=true";
                Log.i("BG-Service", "Getting the URL");
                StringRequest request = new StringRequest(url, new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        Log.i("BG-Service-Response", response);
                        _questionService.CleanCache();
                        addQuestionsToDb(response);
                    }
                }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        Log.i("BG-Service", "There was an error in connecting to the API");
                        Log.i("BG-Service", error.toString());
                    }
                });
                if (_queue != null)
                    _queue.add(request);
                else
                    Log.i("BG-Service", "Queue is empty");
            }
        };
    }

    private void addQuestionsToDb(String responseString) {
        try {
            JSONArray arr = new JSONArray(responseString);
            for (int i = 0; i < arr.length(); i++) {
                JSONObject question = arr.getJSONObject(i);
                String id = question.getString("id");
                String value = question.getString("value");
                String answer = question.getString("correctAnswer");
                int levelId = question.getInt("difficultLevel");
                String level = GetLevel(levelId);

                Question questionObj = new Question(id, value, answer, level);
                _questionService.AddQuestion(questionObj);

                Log.i("BG-Service", id + " Added to DB");
            }
        } catch (JSONException e) {
            Log.i("BG-Service", "There was an error is parsing the JSON");
            e.printStackTrace();
        }
    }

    private String GetLevel(int levelId) {
        if (levelId <= 100)
            return "Basic";
        if (levelId <= 200)
            return "Intermediate";
        return "Advanced";
    }

    public void stopTimerTask() {
        if (timer != null) {
            timer.cancel();
            timer = null;
        }
    }

    @Override
    public IBinder onBind(Intent intent) {
        // TODO: Return the communication channel to the service.
        throw new UnsupportedOperationException("Not yet implemented");
    }
}
