import azure.cognitiveservices.speech as speechsdk
import json
import time

# insert absolute file path to wav file sampled at 16kHz (not mp4!)
my_filename = r'C:\Users\...\fsi-hack4autism\vid1.wav'


# Set up the subscription info for the Speech Service:
# Replace with your own subscription key and service region (e.g., "westus").
speech_key, service_region = "1b8bf0f4084d480bb0d86903ee6501f9", "eastus"

def speech_recognize_continuous_from_file_with_detailed_recognition_results():
    """performs one-shot speech recognition with input from an audio file, showing detailed recognition results
    including word-level timing """
    # <SpeechRecognitionFromFileWithDetailedRecognitionResults>
    speech_config = speechsdk.SpeechConfig(subscription=speech_key, region=service_region)

    # Ask for detailed recognition result
    speech_config.output_format = speechsdk.OutputFormat.Detailed

    # If you also want word-level timing in the detailed recognition results, set the following.
    # Note that if you set the following, you can omit the previous line
    #   "speech_config.output_format = speechsdk.OutputFormat.Detailed",
    # since word-level timing implies detailed recognition results.
    speech_config.request_word_level_timestamps()

    audio_config = speechsdk.audio.AudioConfig(filename=my_filename)

    # Creates a speech recognizer using a file as audio input, also specify the speech language
    speech_recognizer = speechsdk.SpeechRecognizer(
        speech_config=speech_config, language="en-US", audio_config=audio_config)

    # Starts speech recognition, and returns after a single utterance is recognized. The end of a
    # single utterance is determined by listening for silence at the end or until a maximum of 15
    # seconds of audio is processed. It returns the recognition text as result.
    # Note: Since recognize_once() returns only a single utterance, it is suitable only for single
    # shot recognition like command or query.
    # For long-running multi-utterance recognition, use start_continuous_recognition() instead.
    #result = speech_recognizer.recognize_once()
    done = False

    def stop_cb(evt: speechsdk.SessionEventArgs):
        """callback that signals to stop continuous recognition upon receiving an event `evt`"""
        print('CLOSING on {}'.format(evt))
        speech_recognizer.stop_continuous_recognition()
        nonlocal done
        done = True

    all_results = []
    def handle_final_result(evt: speechsdk.SessionEventArgs):
        all_results.append(evt.result)
    speech_recognizer.recognized.connect(handle_final_result)
    # Connect callbacks to the events fired by the speech recognizer
    speech_recognizer.recognizing.connect(lambda evt: print('RECOGNIZING: {}'.format(evt)))
    speech_recognizer.recognized.connect(lambda evt: print('RECOGNIZED: {}'.format(evt)))
    speech_recognizer.session_started.connect(lambda evt: print('SESSION STARTED: {}'.format(evt)))
    speech_recognizer.session_stopped.connect(lambda evt: print('SESSION STOPPED {}'.format(evt)))
    speech_recognizer.canceled.connect(lambda evt: print('CANCELED {}'.format(evt)))
    # stop continuous recognition on either session stopped or canceled events
    speech_recognizer.session_stopped.connect(stop_cb)
    speech_recognizer.canceled.connect(stop_cb)

    # Start continuous speech recognition
    speech_recognizer.start_continuous_recognition()
    while not done:
        time.sleep(.5)

    print("DONE")
    print("=================================================================")

    # Check the result
    for result in all_results:
        if result.reason == speechsdk.ResultReason.RecognizedSpeech:
            print("Recognized: {}".format(result.text))

            # Time units are in hundreds of nanoseconds (HNS), where 10000 HNS equals 1 millisecond
            print("Offset: {}".format(result.offset))
            print("Duration: {}".format(result.duration))

            # Now get the detailed recognition results from the JSON
            json_result = json.loads(result.json)

            # The first cell in the NBest list corresponds to the recognition results
            # (NOT the cell with the highest confidence number!)
            print("Detailed results - Lexical: {}".format(json_result['NBest'][0]['Lexical']))
            # ITN stands for Inverse Text Normalization
            print("Detailed results - ITN: {}".format(json_result['NBest'][0]['ITN']))
            print("Detailed results - MaskedITN: {}".format(json_result['NBest'][0]['MaskedITN']))
            print("Detailed results - Display: {}".format(json_result['NBest'][0]['Display']))

            # Print word-level timing. Time units are HNS.
            words = json_result['NBest'][0]['Words']
            print("Detailed results - Word timing:\nWord:\tOffset:\tDuration:")
            for word in words:
                print(f"{word['Word']}\t{word['Offset']}\t{word['Duration']}")

            # You can access alternative recognition results through json_result['NBest'][i], i=1,2,..

        elif result.reason == speechsdk.ResultReason.NoMatch:
            print("No speech could be recognized: {}".format(result.no_match_details))
        elif result.reason == speechsdk.ResultReason.Canceled:
            cancellation_details = result.cancellation_details
            print("Speech Recognition canceled: {}".format(cancellation_details.reason))
            if cancellation_details.reason == speechsdk.CancellationReason.Error:
                print("Error details: {}".format(cancellation_details.error_details))
        # </SpeechRecognitionFromFileWithDetailedRecognitionResults>

speech_recognize_continuous_from_file_with_detailed_recognition_results()
