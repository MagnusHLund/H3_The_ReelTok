import { StyleSheet, Text, View } from "react-native";
import { ReactNode } from "react";

interface CommentProps {
  username: string,
  profilePicture: ReactNode,
  commentText: string,
}

const Comment: React.FC<CommentProps> = ({ username, profilePicture, commentText  }) => {
  const styles = StyleSheet.create({
    commentContainer: {
      display: 'flex',
      justifyContent: 'flex-start',
      flexDirection: 'row',
      gap: 10,
    },
    commentPicture: {
      display: 'flex',
      justifyContent: 'flex-start',
    },
    commentDetails: {
      gap: 3,
    },
    usernameText: {
      color: 'white',
      fontSize: 12.5,
    },
    commentText: {
      color: 'white',
      fontSize: 14.5,
    }
  });  

  return (
      <>
        <View style={styles.commentContainer}>
          <View style={styles.commentPicture}>
            {profilePicture}
          </View>
          <View style={styles.commentDetails}>
            <Text style={styles.usernameText}>
              {username} 
            </Text>
            <Text style={styles.commentText}>
              {commentText}
            </Text>
          </View>
        </View> 
      </>
  ); 
};

export default Comment;
