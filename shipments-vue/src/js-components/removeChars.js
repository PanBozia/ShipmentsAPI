
const removeChars = () =>{

    let newSentence = null
    const replace =  (sentence) => {
        let sentenceCharArray = sentence.split("")
        
        // console.log(sentenceCharArray)    
        sentenceCharArray.forEach(char => {
            switch (char) {
                case 'ą':
                    char = 'a'
                    break;
                case 'ć':
                    char = 'c'
                    break;
                case 'Ć':
                    char = 'C'
                    break;
                case 'ę':
                    char = 'e'
                    break;
                case 'ł':
                    char = 'l'
                    break;
                case 'Ł':
                    char = 'L'
                    break;
                case 'ń':
                    char = 'n'
                    break;
                case 'ó':
                    char = 'o'
                    break;
                case 'ś':
                    char = 'ś'
                    break;
                case 'Ś':
                    char = 'S'
                    break;
                case 'ź':
                    char = 'z'
                    break;
                case 'Ź':
                    char = 'Z'
                    break;
                case 'ż':
                    char = 'z'
                    break;
                case 'Ż':
                    char = 'Z'
                    break;
            
                default:
                    break;
            }
        newSentence = sentenceCharArray.join("")
        // console.log(newSentence)    
        });
    }
      return {replace, newSentence}
}
export default removeChars