import React, { useState } from 'react';
import { Modal, View, Text, StyleSheet } from 'react-native';
import CustomButton from '../../input/CustomButton';

interface CommentSectionProps {
  comments?: React.ReactNode,
  showComments: boolean, 
}

const CommentsSection: React.FC<CommentSectionProps> = ({comments, showComments}) => {
  
  const styles = StyleSheet.create({
      commentsContainer: {
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
      },
      modalOverlay: {
        display: 'flex',
        justifyContent: 'flex-end', // aligns modal content at the bottom
        backgroundColor: 'rgba(0,0,0,0.5)', // semi-transparent background
      },
      modalContent: {
        backgroundColor: 'white',
        padding: 20,
        borderTopLeftRadius: 15,
        borderTopRightRadius: 15,
      },
      modalTitle: {
        fontSize: 20,
        marginBottom: 10,
      },
      closeButton: {
        fontSize: 18,
        color: 'blue',
        marginTop: 10,
      },
  });  
  
  const [modalVisible, setModalVisible] = useState(showComments);

  return (
    <>
      <View style={styles.commentsContainer}>
        <Modal
          animationType="slide"
          transparent={true}
          visible={modalVisible}
          onRequestClose={() => setModalVisible(false)}
        >
          <View style={styles.modalOverlay}>
            <View style={styles.modalContent}>
              <Text style={styles.modalTitle}>Comments</Text>
              {/* Add your comment content here */}
              <CustomButton onPress={() => setModalVisible(false)}>
                <Text style={styles.closeButton}>Close Comments</Text>
              </CustomButton>
            </View>
          </View>
        </Modal>
      </View>
    </>
  )
};

export default CommentsSection;
