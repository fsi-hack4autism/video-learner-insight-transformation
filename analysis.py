from azure.core.credentials import AzureKeyCredential
from azure.ai.textanalytics import (
    TextAnalyticsClient,
    RecognizeEntitiesAction,
    RecognizeLinkedEntitiesAction,
    RecognizePiiEntitiesAction,
    ExtractKeyPhrasesAction,
    AnalyzeSentimentAction,
)

endpoint = "https://democogserv1.cognitiveservices.azure.com/"
key = "1b8bf0f4084d480bb0d86903ee6501f9"

mock_learner_name = "Drew"

conversation_mock_data1 = [{'speaker1':"Excited. He was excited.",
                            'speaker2':"OK, listen to this little story. It was Barry's birthday and he was on his way home. He had a long day at work. When he opened the door, his friends and family screamed happy birthday and then they threw confetti at him. I'll read it again. OK? Listen, It was Garry's birthday and he was on his way home. He had a long day at work. When he opened the door, his friends and family screamed happy birthday and then they threw confetti at him. How do you think Barry was feeling?"},
                           {'speaker1':"",
                            'speaker2':"It is very surprising. That was Super Berry was excited because it was his birthday. That was good. I think that Barry was surprised. I don't think that he knew there were gonna be people at his house having a party for him. I think Barry was surprised when they screamed. That was great, Drew."}]

conversation_mock_data2 = [{'speaker1':"He was feeling sick. With the 650 crabby patties.",
                            'speaker2':"Hey, listen to this little story Drew. SpongeBob ate 50 crabby patties. He burped and rubbed his belly. SpongeBob could barely move. Listen, I'll read it again. SpongeBob 850 Crabby patties. He burped and rubbed his belly. Sponge Bob could barely move. How do you think he was feeling?"},
                           {'speaker1':"",
                            'speaker2':"Well that was great. Putting it in complete sentence, I think SpongeBob was feeling stuffed. He was full. He ate way too much. That was great."}]

mock_data = [conversation_mock_data1, conversation_mock_data2]

# Authenticate the client using your key and endpoint 
def authenticate_client():
    ta_credential = AzureKeyCredential(key)
    text_analytics_client = TextAnalyticsClient(
            endpoint=endpoint, 
            credential=ta_credential)
    return text_analytics_client

##
def key_phrase_extraction(client, input):
    try:
        response = client.extract_key_phrases(documents = [input])[0]

        if not response.is_error:
            return response.key_phrases
        else:
            print(response.id, response.error)

    except Exception as err:
        print("Encountered exception. {}".format(err))

    return []

def key_phrase_comparison(input_learner, input_user):

    score = 1.0

    # TODO: assuming we can capture when learner is not responding
    if len(input_learner) == 0:
        return 0.0

    # if learner responded with one word only
    if len(input_learner) == 1:
        score -= 0.3

    # common keywords used between learner and user
    if float(len(set(input_learner) & set(input_user))) / float(len(set(input_learner))) < 0.2:
        score -= 0.3

    # TODO: check if keywords are synonyms between learner and user

    return score


def sentiment_analysis(client, input):

    result = client.analyze_sentiment([input], show_opinion_mining=True)
    doc_result = [doc for doc in result if not doc.is_error]
    if len(doc_result) == 0:
        return {'pos_score':0.0, 'neg_score':0.0, 'neutral_score':0.0, 'overall_sentiment':None, 'max_type':None}
    else:
        doc_result = doc_result[0]
    
    data = {}

    data['pos_score'] = doc_result.confidence_scores.positive
    data['neutral_score'] = doc_result.confidence_scores.neutral
    data['neg_score'] = doc_result.confidence_scores.negative
    data['overall_sentiment'] = doc_result.sentiment
    
    max_value = 0.0
    max_type = ''
    score_types = ['pos_score', 'neutral_score', 'neg_score']
    for type in score_types:
        if data[type] > max_value:
            max_type = type
    data['max_type'] = max_type

    return data

def sentiment_comparison(input_learner, input_user):

    similarity_score = 1.0

    if input_learner['overall_sentiment'] != input_user['overall_sentiment']:
        similarity_score -= 0.3

    # no response from learner
    if input_learner['max_type'] == None:
        return 0.0

    if abs(input_learner[input_learner['max_type']] - input_user[input_user['max_type']]) > 0.2:
        similarity_score -= 0.1

    return similarity_score

def main():
    
    client = authenticate_client()
    user_sentiment_history = 0.0

    #TODO: get video indexer json data and process it
    
    #TODO: split conversation transcript into interactions based on when learner responds
    
    for i in range(2):
        conversation_mock_data = mock_data[i]
        for i in range(len(conversation_mock_data)):
            # Print out interaction
            print("-----------Outputting transcript of this interaction----------")
            print(conversation_mock_data[i])

            # compare sentiment between speakers in each interaction --> maybe create a graph that indicates how well the conversation is going
            print("--------Calculating sentiment score for this interaction--------")
            learner_data = sentiment_analysis(client, conversation_mock_data[i]["speaker1"])
            user_data = sentiment_analysis(client, conversation_mock_data[i]["speaker2"])
            sentiment_score = sentiment_comparison(learner_data, user_data)
            print("Sentiment Similarity Score: ", sentiment_score)

            # keeping track of sentiment across interactions
            user_sentiment_history += sentiment_score

            # TODO: identify sentences with learner name and calculate sentiment of that sentence
            user_sentences = conversation_mock_data[i]["speaker2"].split('.')
            important_sentences = [sent for sent in user_sentences if sent.find(mock_learner_name)!=-1]
            user_specific_sentiment = sentiment_analysis(client, '. '.join(important_sentences))
            print("Important sentences identified: ", important_sentences)
            print("Sentiment of important sentences: ", user_specific_sentiment['overall_sentiment'])

            # calculate how well learner is responding --> content of reponse analysis
            print("---------Calculating how well learner is responding in this interaction----------")
            learner_data = key_phrase_extraction(client, conversation_mock_data[i]["speaker1"])
            user_data = key_phrase_extraction(client, conversation_mock_data[i]["speaker2"])
            phrase_score = key_phrase_comparison(learner_data, user_data)
            print("Key Phrases Used Similarity Score: ", phrase_score)

            print("\n\n")


    #TODO: calculate therapist/parent perception of how learner is performing --> track number of positive and negative reviews spoken by therapist/parent
    print("--------Overall Calculating Therapist/Parent User Perception of how learner is performing----------")
    print("overall cumulative review score: ", user_sentiment_history/float(len(mock_data)))
    

main()
