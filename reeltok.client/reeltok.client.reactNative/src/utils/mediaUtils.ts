export const isImage = (uri: string) => {
  const imageExtensions = ['.jpg', '.jpeg', '.png', '.gif']
  return imageExtensions.some((ext) => uri.toLowerCase().endsWith(ext))
}
