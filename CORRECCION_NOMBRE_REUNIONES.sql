select * from sc_spring_meeting_type;

UPDATE  sc_spring_meeting_type SET nombre='SPRINT DAILY' WHERE id_meeting_type=1;
UPDATE  sc_spring_meeting_type SET nombre='SPRINT REVIEW' WHERE id_meeting_type=2;
UPDATE  sc_spring_meeting_type SET nombre='SPRINT RETROSPECTIVE' WHERE id_meeting_type=3;