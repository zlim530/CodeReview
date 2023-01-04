export interface BlogUpdateDto {
  /**
   * ����
   */
  title?: string | null;
  /**
   * ��Ҫ
   */
  summary?: string | null;
  /**
   * ����
   */
  content?: string | null;
  systemUserId: string;

}
