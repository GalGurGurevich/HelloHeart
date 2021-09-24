export function regValidate(val) {
    const myRegExp = /^[ A-Za-z"(),-:/!]*$/;
    if(myRegExp.test(val)) {
      return true;
    }
    return false;
  }

export function enumToText(number) {
    switch (number) {
      case 0: return "Unknown :S"
      case 1: return "Good :)"
      case 2: return "Bad :(";
      default:
        break;
    }
  }
